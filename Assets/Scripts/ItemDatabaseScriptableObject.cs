using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatabase",menuName ="BGS/ItemDatabase")]
public class ItemDatabaseScriptableObject : ScriptableObject
{
    [SerializeField]private Item[] _itemDatabase;
}