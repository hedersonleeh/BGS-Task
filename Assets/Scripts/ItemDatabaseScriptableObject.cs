using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatabase",menuName ="BGS/ItemDatabase")]
public class ItemDatabaseScriptableObject : ScriptableObject
{
    [SerializeField]private List<ItemData> _itemDatabase;

    public ItemData GetItem(string id)
    {
      var found=  _itemDatabase.Find((item) => item.id == id);
        Debug.Assert(!string.IsNullOrEmpty( found.id), $"Item not found on database id:{id}");
        return found; //retrun a copy
    }
}