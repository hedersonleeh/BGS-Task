using UnityEngine;
public class Item
{
    public enum Type
    {

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