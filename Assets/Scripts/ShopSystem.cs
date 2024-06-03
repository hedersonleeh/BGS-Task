using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShopMVC
{
    public class ShopSystem : MonoBehaviour
    {
        [SerializeField] private ShopView _view;
        [SerializeField] private ItemDatabaseScriptableObject _database;
        ShopModel _model;
        ShopController _controller;
        public bool _open;

        private void Awake()
        {
            var player = FindObjectOfType<PlayerController>();

            _model = new ShopModel(_database.GetItems());
            _controller = new ShopController(player, _model, _view);

            _view.FillItems(_model.inventory);
            _view.gameObject.SetActive(false);

            _controller.onShopCloseEvent += CloseShop;
        }
        private void OnDestroy()
        {
            _controller.onShopCloseEvent -= CloseShop;
        }
        [ContextMenu("Test Open")]
        public void OpenShop()
        {
            _open = true;
            _controller.OpenCloseShop(_open);
            GlobalVariables.PlayerIsBusy = true;
        }
        [ContextMenu("Test close")]
        public void CloseShop()
        {
            _open = false;
            _controller.OpenCloseShop(_open);
            GlobalVariables.PlayerIsBusy = false;
        }
        public void GoToState(int newState)
        {
            _model.ChangeState((State)newState);
        }
        private void Update()
        {
            if (_open)
                _controller.InputUpdate();
        }
    }
}
