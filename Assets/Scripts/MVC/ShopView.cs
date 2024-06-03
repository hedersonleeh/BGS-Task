using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopMVC
{

    public class ShopView : ViewBase
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private ItemSlot _slotPrefab;
        [SerializeField] private GameObject[] _shopScreens;
        [SerializeField] private Image _itemIconConfirmationWindow;
        [SerializeField] private TextMeshProUGUI _itemDescriptionDisplay;
        [SerializeField] private TextMeshProUGUI _itemNameDisplay;


        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }
      

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
            {
                if (child == _selector) continue;
                Destroy(child.gameObject);
            }
            foreach (var item in items)
            {
                var itemSlot = Instantiate(_slotPrefab, content);
                itemSlot.FillInfo(item);
            }
        }
        public override void OnItemSelect(ItemSlot slot) 
        {
            base.OnItemSelect(slot);
        }
        public override void OnItemHover(ItemSlot slot)
        {
            int index = slot.transform.GetSiblingIndex();
            var rt = _scrollRect.content.transform.GetChild(index);
            _selector.position = rt.position;
            base.OnItemHover(slot);
            currentItemSelectedIndex = index;
        }
        public void HoverOverItem(int index)
        {
            var rt = _scrollRect.content.transform.GetChild(index);
            OnItemHover(rt.GetComponent<ItemSlot>());
        }

        public void ShowConfirmationWindow(ItemSlot slot)
        {
            _itemIconConfirmationWindow.sprite = slot.data.icon;
            _itemIconConfirmationWindow.color = slot.data.tint;
            _itemIconConfirmationWindow.enabled = slot.data.icon != null;
            _itemNameDisplay.text= slot.data.displayName;
            _itemDescriptionDisplay.text = slot.data.description;
        }
       
        public void OnShopStateChange(State newState)
        {
            foreach (var screen in _shopScreens)
            {
                screen.gameObject.SetActive(false);
            }
            _shopScreens[(int)newState].gameObject.SetActive(true);
            
        }

    }
}
