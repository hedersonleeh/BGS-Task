using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatabase",menuName ="BGS/ItemDatabase")]
public class ItemDatabaseScriptableObject : ScriptableObject
{
    [SerializeField]private List<ItemData> _itemDatabase;

    public List<ItemData> GetItems() =>new List<ItemData>( _itemDatabase);
    public List<ItemData> GetItemsByType(ItemData.Type type) => new List<ItemData>(  _itemDatabase.FindAll(x=>x.type == type));
    public ItemData GetItem(string id)
    {
      var found=  _itemDatabase.Find((item) => item.ID == id);
        Debug.Assert(!string.IsNullOrEmpty( found.ID), $"Item not found on database id:{id}");
        return found; //retrun a copy
    }
}