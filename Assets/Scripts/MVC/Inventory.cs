using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int money { get; private set; }
    private Dictionary<string, ItemData> _inventory;
    public Inventory(int initialMoney, List<ItemData> items)
    {
        money = initialMoney;
        _inventory = new Dictionary<string, ItemData>();
        foreach (var item in items)
        {
            if (!_inventory.ContainsKey(item.ID))
            {
                _inventory.Add(item.ID, item);
            }
            else
            {
                var invetoryItem = _inventory[item.ID];
                invetoryItem.quantity++;
                _inventory[item.ID] = invetoryItem;
            }
        }
    }
    public List<ItemData> GetItems() => _inventory.Values.ToList();
    public void AddItem(ItemData newItem)
    {
        if (!_inventory.ContainsKey(newItem.ID))
        {
            _inventory.Add(newItem.ID, newItem);
        }       
    }
    public bool DiscardItem(string id)
    {
        if (_inventory.TryGetValue(id,out var item))
        {
            _inventory.Remove(item.ID);
            return true;
        }
        return false;
    }
    public int SpendMoney(int amount)
    {
        money = Mathf.Max(0, money - amount);
        return money;
    }
    public int GainMoney(int amount)
    {
        money = Mathf.Max(0, money + amount);
        return money;
    }
}