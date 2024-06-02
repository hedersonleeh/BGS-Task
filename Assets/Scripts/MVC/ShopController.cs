using System;
using UnityEngine;

namespace ShopMVC
{
    public class ShopController
    {
        private ShopModel _shopModel;
        private ShopView _shopView;

        public ShopController(ShopModel shopModel, ShopView shopView)
        {
            _shopModel = shopModel;
            _shopView = shopView;
        }

        public void InputUpdate()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var x = (int)Input.GetAxisRaw("Horizontal");
                var next = _shopView.currentItemSelectedIndex + x;
                _shopView.HoverOverItem(next);
            }
            if (Input.GetButtonDown("Vertical"))
            {
                var y = (int)Input.GetAxisRaw("Vertical");
                var next =Mathf.Min( _shopView.currentItemSelectedIndex + (y*7),_shopView.content.childCount-1);
                _shopView.HoverOverItem(next);
            }
        }
        
    }
}