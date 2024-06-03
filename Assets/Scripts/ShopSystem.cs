using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShopMVC
{
    public class ShopSystem : MonoBehaviour
    {
        [SerializeField] private ShopView _view;
        ShopModel _model;
        ShopController _controller;
        public bool _open;

        private void Awake()
        {
            _model = new ShopModel();
            var player = FindObjectOfType<PlayerController>();
            _controller = new ShopController(player, _model, _view);
            _controller.onShopCloseEvent += CloseShop;
            _view.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _controller.onShopCloseEvent -= CloseShop;
        }
        [ContextMenu("Test Open")]
        public void OpenShop()
        {
            _open = true;
            _view.gameObject.SetActive(_open);
            GlobalVariables.PlayerIsBusy = true;

        }
        [ContextMenu("Test close")]
        public void CloseShop()
        {
            _open = false;
            _view.gameObject.SetActive(_open);
            GlobalVariables.PlayerIsBusy = false;
        }
        public void GoToState(int newState)
        {
            _model.ChangeState((State) newState);
        }
        private void Update()
        {
            if (_open)
                _controller.InputUpdate();
        }

    }
}
