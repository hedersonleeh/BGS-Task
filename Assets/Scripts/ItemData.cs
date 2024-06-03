using UnityEngine;
[System.Serializable]
public struct ItemData
{
    [System.Serializable]
    public enum Type
    {
        HAT,
        CLOTHES,
        HAIR,
    }
    public string ID => icon?.name +displayName+tint.ToString();
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
