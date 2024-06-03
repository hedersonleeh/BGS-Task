using System;
using UnityEngine;

namespace ShopMVC
{
    public class ShopController
    {
        private ShopModel _shopModel;
        private ShopView _shopView;
        private PlayerController _player;

        public delegate void OnShopClose();
        public OnShopClose onShopCloseEvent;
        public ShopController(PlayerController player, ShopModel shopModel, ShopView shopView)
        {
            _shopModel = shopModel;
            _shopView = shopView;
            _player = player;
            _shopModel.onStateChange += _shopView.OnShopStateChange;
            _shopView.selectEvent += OnSelectItem;
            onShopCloseEvent += OnClose;
        }
        ~ShopController()
        {
            _shopModel.onStateChange -= _shopView.OnShopStateChange;
            _shopView.selectEvent -= OnSelectItem;
            onShopCloseEvent -= OnClose;

        }
        public void InputUpdate()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var x = (int)Input.GetAxisRaw("Horizontal");
                var next = Mathf.Clamp(_shopView.currentItemSelectedIndex + x, 0, _shopView.content.childCount - 2);
                _shopView.HoverOverItem(next);
            }
            if (Input.GetButtonDown("Vertical"))
            {
                var y = (int)Input.GetAxisRaw("Vertical");
                var next = Mathf.Max(Mathf.Min(_shopView.currentItemSelectedIndex - (y * 7), (_shopView.content.childCount - 2)), 0);
                _shopView.HoverOverItem(next);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
            {
                onShopCloseEvent?.Invoke();
            }
            if (Input.GetButtonDown("Jump"))
            {
                var selected = _shopView.content.GetChild(_shopView.currentItemSelectedIndex);
                var slot = selected.GetComponent<ItemSlot>();
                OnSelectItem(slot);
            }
        }

        private void OnSelectItem(ItemSlot slot)
        {
            switch (_shopModel.CurrentState)
            {
                case State.OPTIONS:

                    break;
                case State.BUY:
                    _shopModel.ChangeState(State.CONFIRM);
                    _shopView.ShowConfirmationWindow(slot);
                   _shopModel.currentItemSelected = slot.data;
                    break;

            }
        }
        private void OnClose()
        {
            _shopModel.ChangeState(State.OPTIONS);
        }
        public void BuyItem()
        {
            if (_shopModel.CurrentState == State.CONFIRM)
            {
                if (_shopModel.TryBuyItem(_player.inventory, _shopModel.currentItemSelected))
                {
                }
                else
                {
                    Debug.LogError("Cant buy it");
                }
            }
        }
    }
}
