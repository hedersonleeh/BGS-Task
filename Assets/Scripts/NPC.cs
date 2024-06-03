using UnityEngine;

public class NPC : MonoBehaviour, IInterable
{
    [SerializeField] private ShopMVC.ShopSystem _shop;
    public void Interact()
    {
        _shop.OpenShop();
    }
}