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
            _controller = new ShopController(_model, _view);
        }
        [ContextMenu("Test Open")]
        public void OpenShop()
        {
            _open = true;
            _view.gameObject.SetActive(_open);
        }
        [ContextMenu("Test close")]
        public void CloseShop()
        {
            _open = false;
            _view.gameObject.SetActive(_open);
        }
        private void Update()
        {
            if (_open)
                _controller.InputUpdate();
        }
    }
}