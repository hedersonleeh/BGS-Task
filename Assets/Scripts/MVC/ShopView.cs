using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopMVC
{
    public delegate void OnItemHoverEvent(ItemSlot slot);
    public delegate void OnItemSelectEvent(ItemSlot slot);
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private ItemSlot _slotPrefab;
        [SerializeField] private GameObject[] _shopScreens;
        [SerializeField] private TextMeshProUGUI _itemDescriptionDisplay;
        [SerializeField] private TextMeshProUGUI _itemNameDisplay;


        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }
        public OnItemHoverEvent hoverEvent;
        public OnItemSelectEvent selectEvent;

        private void OnEnable()
        {
            foreach (var screen in _shopScreens)
            {
                screen.gameObject.SetActive(false);
            }
            _shopScreens[0].gameObject.SetActive(true);
        }
        public void FillItems(List<ItemData> items)
        {
            foreach (Transform child in content.transform)
                Destroy(child.gameObject);
            foreach (var item in items)
            {
                var itemSlot = Instantiate(_slotPrefab, content);
                itemSlot.FillInfo(item);
            }
        }
        public void OnItemSelect(ItemSlot slot) 
        { 
            selectEvent?.Invoke(slot);
        }
        public void OnItemHover(ItemSlot slot)
        {
            int index = slot.transform.GetSiblingIndex();
            var rt = _scrollRect.content.transform.GetChild(index);
            _selector.position = rt.position;
            hoverEvent?.Invoke(slot);
            currentItemSelectedIndex = index;

        }
        public void HoverOverItem(int index)
        {
            var rt = _scrollRect.content.transform.GetChild(index);
            OnItemHover(rt.GetComponent<ItemSlot>());
        }

        public void ShowConfirmationWindow(ItemSlot slot)
        {

        }
       
        public void OnShopStateChange(State newState)
        {
            foreach (var screen in _shopScreens)
            {
                screen.gameObject.SetActive(false);
            }
            _shopScreens[(int)newState].gameObject.SetActive(true);
            switch (newState)
            {
                case State.OPTIONS:
                    break;
                case State.BUY:
                    break;
                case State.CONFIRM:
                    break;
            }
        }

    }
}
