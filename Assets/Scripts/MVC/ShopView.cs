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
        private ShopController _controller;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _selector;
        [SerializeField] private ItemSlot _slotPrefab;
        [SerializeField] private GameObject[] _shopScreens;
        [SerializeField] private ConfirmationWindow _confirmationWindow;
        [SerializeField] private TextMeshProUGUI _Menutitle;
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private Button _optionBuyButton;
        [SerializeField] private Button _optionSellButton;


        public RectTransform content { get { return _scrollRect.content; } }
        public int currentItemSelectedIndex { get; private set; }

        public void AssingController(ShopController controller)
        {
            _controller = controller;
        }
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
                Destroy(child.gameObject);
            }
            foreach (var item in items)
            {
                var itemSlot = Instantiate(_slotPrefab, content);
                itemSlot.FillInfo(item);
            }
            HoverItem(0);
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
            currentItemSelectedIndex = index;
            base.OnItemHover(slot);
        }
        public void HoverItem(int index)
        {
            var rt = _scrollRect.content.transform.GetChild(index);

            OnItemHover(rt.GetComponent<ItemSlot>());
        }

        public void ShowConfirmationWindow(ItemSlot slot, string confirmMessage, System.Action confirmCallback, System.Action cancelCallback)
        {
            _confirmationWindow.Updateinfo(confirmMessage, slot.data);
            _confirmationWindow.AssingCallbacks(confirmCallback, cancelCallback);
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
                    _Menutitle.text = "Shopkeeper";
                    break;
                case State.BUY:
                    _Menutitle.text = "Shop";
                    break;
                case State.SELL:
                    _Menutitle.text = "Selling";
                    break;
                case State.CONFIRM:
                    var rt = _scrollRect.content.transform.GetChild(currentItemSelectedIndex);

                    var slot=rt.GetComponent<ItemSlot>();
                    _Menutitle.text = "PRICE: "+ slot.data.price.ToString()+"$";
                    break;
                default:
                    break;
            }
        }

        public void DisableButtons(bool disableBuyButton, bool disableSell)
        {
            _optionBuyButton.interactable = !disableBuyButton;
            _optionSellButton.interactable = !disableSell;
        }
        public void UpdatePlayerMoney(string money)
        {
            _moneyText.text = "Money: " + money + "$";
        }
        public void OnBack()
        {
            _controller.Back();
        }

    }
}
