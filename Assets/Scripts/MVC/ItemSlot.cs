﻿using ShopMVC;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private Image _a;
    private ShopView _view;
    private void Awake()
    {
        _view = GetComponentInParent<ShopView>();
    }
    public void FillInfo( ItemData data)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _view.OnItemHover(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        _view.OnItemSelect(this);
    }
}