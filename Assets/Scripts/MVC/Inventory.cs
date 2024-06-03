using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public int money { get; private set; }
    private List<ItemData> _inventory;
    public Inventory(int initialMoney,List<ItemData> items)
    {
        money = initialMoney;
        _inventory = new List<ItemData>(items);
    }
    public List<ItemData> GetInvetory() => _inventory;
}