﻿using ShopMVC;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private Image _iconDisplay;
    [SerializeField] private TextMeshProUGUI _nameDisplay;
    [SerializeField] private GameObject _priceTag;
    private ViewBase _view;
    public ItemData data { get; private set; }
    private void Awake()
    {
        _view = GetComponentInParent<ViewBase>();
    }
    public void FillInfo(ItemData data, bool displayPrice = true)
    {
        this.data = data;
        _iconDisplay.sprite = data.icon;
        _iconDisplay.enabled = data.icon != null;
        _nameDisplay.text = data.displayName;
        _iconDisplay.color = data.tint;
        _priceTag.gameObject.SetActive(displayPrice);
        if (displayPrice)
        {
            _priceTag.GetComponentInChildren<TextMeshProUGUI>().text = data.price.ToString() + "$";
        }

        //workaround to adjust the size according to assets
        switch (data.type)
        {
            case ItemData.Type.HAT:
                _iconDisplay.transform.localScale = Vector3.one * 3;
                _iconDisplay.rectTransform.anchoredPosition = Vector2.down * 70;
                break;
            case ItemData.Type.CLOTHES:
                _iconDisplay.transform.localScale = Vector3.one * 2;
                break;
            default:
                break;
        }
    }

    public void Clear()
    {
        _iconDisplay.sprite = null;
        _iconDisplay.enabled = false;
        _nameDisplay.text = "";
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