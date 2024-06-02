using System;
using System.Collections.Generic;
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
        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }
        public OnItemHoverEvent hoverEvent { get; private set; }
        public OnItemSelectEvent selectEvent { get; private set; }

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
            
        }

        
    }
}
