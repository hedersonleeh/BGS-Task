using UnityEngine;
[System.Serializable]
public struct ItemData
{
    [System.Serializable]
    public enum Type
    {
        WEAPON,
        CLOTHES,
        CONSUMABLE,
    }
    public string ID => icon.name;
    public Sprite icon;
    public Type type;
    public int quantity;
    public int price;
    public string displayName;
    public string description;
    public Color tint;
    [SerializeField] private Texture2D _atlas;

    public Texture2D GetAtlas()
    {
        return _atlas;
    }
}
