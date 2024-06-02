using UnityEngine;
[System.Serializable]public class Item
{
    [System.Serializable]
    public enum Type
    {
        WEAPON,
        CLOTHES,
        CONSUMABLE,
    }
    public Texture icon;
    public string id;
    public Type type;
    public int quantity;
    public int price;
    public string displayName;
    public string description;
    [TextArea]public string extraParams;
}
