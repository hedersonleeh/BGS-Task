using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMVC
{
    public class InventoryView : ViewBase
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private ItemSlot _bodySlot;
        [SerializeField] private ItemSlot _hatSlot;
        [SerializeField] private ItemSlot _hairSlot;
        List<ItemSlot> _items;

        [SerializeField] private ItemSlot _slotPrefab;
        private InventoryModel _model;
        private InventoryController _controller;
        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }
        private void Awake()
        {
            var player = FindObjectOfType<PlayerController>();
            var playerClothes = player.GetComponent<PlayerClothes>();
            _items = new List<ItemSlot>();
            _model = new InventoryModel(player.inventory);
            _controller = new InventoryController(this, _model, playerClothes);
            _model.onEquip += SwapItems;
            selectEvent += _model.OnItemSelect;
            _model.unEquip += OnUnEquip;
            foreach (Transform child in _scrollRect.content.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ToggleView(bool value)
        {
            GlobalVariables.PlayerIsBusy = value;
            gameObject.SetActive(value);
        }
        private void OnEnable()
        {
            _controller.OnOpenInventory();
        }
        private void OnDestroy()
        {
            _model.onEquip -= SwapItems;
            selectEvent -= _model.OnItemSelect;
            _model.unEquip -= OnUnEquip;

        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                ToggleView(false);
        }

        public void FillItems(List<ItemData> items)
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();

            foreach (var item in items)
            {
                if (InventoryModel.isEquipped(item))
                {
                    continue;
                }

                var itemSlot = Instantiate(_slotPrefab, content);
                itemSlot.FillInfo(item, false);
                _items.Add(itemSlot);
            }
            HoverItem(0);
        }

        public void HoverItem(int index)
        {
            var rt = _scrollRect.content.transform.GetChild(index);
            OnItemHover(rt.GetComponent<ItemSlot>());
        }

        public void SwapItems(ItemData newItem)
        {
            var item1 = _items.Find(x => newItem.ID == x.data.ID);

            if (item1)
            {
                switch (newItem.type)
                {
                    case ItemData.Type.HAT:
                        _hatSlot.FillInfo(item1.data, false);
                        break;
                    case ItemData.Type.CLOTHES:
                        _bodySlot.FillInfo(item1.data, false);
                        break;
                    case ItemData.Type.HAIR:
                        _hairSlot.FillInfo(item1.data, false);
                        break;
                }
                UpdateView();
            }
        }

        private void OnUnEquip(ItemData equip)
        {
            var item = _items.Find(x => equip.ID == x.data.ID);
           
            UpdateView();
        }

        private void UpdateView()
        {
            foreach (var item in _items)
            {
                item.gameObject.SetActive(!InventoryModel.isEquipped(item.data));
            }            
        }
    }
}