using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMVC
{
    public class InventoryView : ViewBase
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private RectTransform _bodySlot;
        [SerializeField] private RectTransform _hatSlot;
        [SerializeField] private RectTransform _hairSlot;
        List<ItemSlot> _items;

        private ItemSlot _slotPrefab;
        private InventoryModel _model;
        private InventoryController _controller;
        private void Awake()
        {
            var player = FindObjectOfType<PlayerController>();
            var playerClothes = player.GetComponent<PlayerClothes>();
            _items = new List<ItemSlot>();

            _model = new InventoryModel(player.inventory);
            _controller = new InventoryController(this, _model, playerClothes);
            _model.onEquip += SwapItems;
            selectEvent += _model.OnItemSelect;
        }
        private void OnDestroy()
        {
            _model.onEquip -= SwapItems;
            selectEvent -= _model.OnItemSelect;
        }
        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }

        public override void OnItemSelect(ItemSlot slot)
        {
            selectEvent?.Invoke(slot);
        }
        public override void OnItemHover(ItemSlot slot)
        {
            hoverEvent?.Invoke(slot);
        }
        public void FillItems(List<ItemData> items)
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }
            _items.Clear();
            foreach (var item in items)
            {
                var itemSlot = Instantiate(_slotPrefab, content);
                itemSlot.FillInfo(item);
                _items.Add(itemSlot);
            }
            HoverItem(0);
        }

        public void HoverItem(int index)
        {
            var rt = _scrollRect.content.transform.GetChild(index);
            OnItemHover(rt.GetComponent<ItemSlot>());
        }
        public void SwapItems(ItemData newItem, ItemData oldItem)
        {
            var item1 = _items.Find(x => newItem.ID == x.data.ID);
            if (!string.IsNullOrEmpty(oldItem.ID))
            {
                var item2 = _items.Find(x => oldItem.ID == x.data.ID);
                StartCoroutine(MoveToSmooth(item2, _scrollRect.content));
            }
            switch (newItem.type)
            {
                case ItemData.Type.HAT:
                    StartCoroutine(MoveToSmooth(item1, _hatSlot));
                    break;
                case ItemData.Type.CLOTHES:
                    StartCoroutine(MoveToSmooth(item1, _bodySlot));
                    break;
                case ItemData.Type.HAIR:
                    StartCoroutine(MoveToSmooth(item1, _hairSlot));
                    break;
            }
        }
        IEnumerator MoveToSmooth(ItemSlot item, RectTransform targetRectTranform)
        {
            item.transform.parent = null;
            var initialPosition = item.transform.position;
            var finalPosition = targetRectTranform.transform.position;
            var stepTime = 0f;
            var duration = 0.5f;
            AnimationCurve smooth = AnimationCurve.EaseInOut(0, 0, 1, 1);
            while (stepTime < duration)
            {
                stepTime += Time.deltaTime;
                var fixedDuration = stepTime / duration;
                item.transform.position = Vector3.Lerp(initialPosition, finalPosition, smooth.Evaluate(fixedDuration));
                yield return new WaitForEndOfFrame();
            }
            item.transform.parent = targetRectTranform;
        }

    }
}