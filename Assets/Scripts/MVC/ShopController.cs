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
            _shopModel.onStateChange += OnShopStateChange;
            _shopView.selectEvent += OnSelectItem;
            onShopCloseEvent += OnClose;
            _shopModel.onInventoryUpdate += OnInventoryUpdateReaction;
        }

        ~ShopController()
        {
            _shopModel.onStateChange -= _shopView.OnShopStateChange;
            onShopCloseEvent -= OnClose;
            _shopView.selectEvent -= OnSelectItem;
            _shopModel.onStateChange -= OnShopStateChange;

        }



        public void InputUpdate()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var x = (int)Input.GetAxisRaw("Horizontal");
                var next = Mathf.Clamp(_shopView.currentItemSelectedIndex + x, 0, _shopView.content.childCount - 2);
                _shopView.HoverItem(next);
            }
            if (Input.GetButtonDown("Vertical"))
            {
                var y = (int)Input.GetAxisRaw("Vertical");
                var next = Mathf.Max(Mathf.Min(_shopView.currentItemSelectedIndex - (y * 7), (_shopView.content.childCount - 2)), 0);
                _shopView.HoverItem(next);
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
            {
                switch (_shopModel.CurrentState)
                {
                    case State.OPTIONS:
                        onShopCloseEvent?.Invoke();
                        break;
                    case State.BUY:
                    case State.SELL:
                        _shopModel.ChangeState(State.OPTIONS);
                        break;
                    case State.CONFIRM:
                        _shopModel.ChangeState(_shopModel.LastState);
                        break;
                    default:
                        break;
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (_shopModel.CurrentState == State.BUY || _shopModel.CurrentState == State.SELL)
                {
                    var selected = _shopView.content.GetChild(_shopView.currentItemSelectedIndex);
                    var slot = selected.GetComponent<ItemSlot>();
                    OnSelectItem(slot);
                }
            }
        }
        private void OnSelectItem(ItemSlot slot)
        {
            _shopModel.currentItemSelected = slot.data;

            switch (_shopModel.CurrentState)
            {
                case State.OPTIONS:

                    break;
                case State.BUY:
                    _shopModel.ChangeState(State.CONFIRM);

                    _shopView.ShowConfirmationWindow(slot, "Buy", () =>
                     {
                         BuyItem();
                     }, () =>
                     {
                         if (_shopModel.inventory.Count <= 0)
                         {
                             _shopModel.ChangeState(State.OPTIONS);
                             return;
                         }
                         _shopModel.ChangeState(State.BUY);
                     });
                    break;
                case State.SELL:
                    _shopModel.ChangeState(State.CONFIRM);

                    _shopView.ShowConfirmationWindow(slot, "Sell", () =>
                    {
                        SellItem();
                    }, () =>
                    {
                        if (_player.inventory.GetItems().Count <= 0)
                        {

                            _shopModel.ChangeState(State.OPTIONS);
                            return;
                        }
                        _shopModel.ChangeState(State.SELL);
                    });
                    break;
            }
        }

        private void OnClose()
        {
            _shopModel.ChangeState(State.OPTIONS);
        }

        private void OnInventoryUpdateReaction()
        {
            if (_shopModel.inventory.Count <= 0)
                _shopModel.ChangeState(State.OPTIONS);
            else
            {
                switch (_shopModel.CurrentState)
                {
                    case State.BUY:
                        _shopView.FillItems(_shopModel.inventory);
                        break;
                    case State.SELL:
                        _shopView.FillItems(_player.inventory.GetItems());
                        break;
                }
            }
            _shopView.UpdatePlayerMoney(_player.inventory.money.ToString());
        }

        private void OnShopStateChange(State newState)
        {
            switch (newState)
            {
                case State.OPTIONS:
                    _shopView.DisableButtons(_shopModel.inventory.Count <= 0, _player.inventory.GetItems().Count <= 0);
                    break;
                case State.BUY:
                    _shopView.FillItems(_shopModel.inventory);
                    break;
                case State.SELL:
                    _shopView.FillItems(_player.inventory.GetItems());
                    break;
                case State.CONFIRM:
                    break;
            }
            _shopView.UpdatePlayerMoney(_player.inventory.money.ToString());

        }

        public void BuyItem()
        {
            if (_shopModel.CurrentState == State.CONFIRM)
            {
                if (_shopModel.TryBuyItem(_player.inventory, _shopModel.currentItemSelected))
                {
                    Debug.Log("Buy confirmed new money " + _player.inventory.money + "$");
                    _shopModel.ChangeState(_shopModel.LastState);
                }
                else
                {
                    Debug.LogError("Cant buy it");
                }
            }
        }
        public void SellItem()
        {
            if (_shopModel.CurrentState == State.CONFIRM)
            {
                _shopModel.SellItem(_player.inventory, _shopModel.currentItemSelected);
                _shopModel.ChangeState(_shopModel.LastState);
            }
        }
        public void OpenCloseShop(bool value)
        {
            _shopModel.ChangeState(State.OPTIONS);
            _shopView.gameObject.SetActive(value);
        }
    }
}
