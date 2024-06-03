using UnityEngine;
using UnityEngine.U2D.Animation;

public class MultiLayerCharacterRenderer : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [SerializeField] private Texture2D _atlas;
    private int lastIndex = -1;
    Rect rect = new Rect(Vector2.zero, Vector2.one * 64);
    readonly Vector2 pivot = new Vector2(0.5f, 0.3f);
    private int Row=8, Column=8;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void AssingAtlas( Texture2D atlas)
    {
        _atlas = atlas;
        lastIndex = -1;
    }
    public void Clear() 
    {
        _renderer.sprite = null;
        AssingAtlas(null);
    }
    public void Tint(Color tint)
    {
        _renderer.color = tint;
           
    }
    public void UpdateAnimation(bool Xflip,int index)
    {
        if (_atlas == null || _renderer == null) return;
        if (lastIndex == index) return;
        lastIndex = index;
        rect.x = (index % Row) * (rect.width);
        rect.y = ((Column-1) - Mathf.Floor(index/ Column)) * (rect.height);//from bottom to top
        var sprite = Sprite.Create(_atlas, rect, pivot, 25);
        sprite.name = index.ToString();
        _renderer.sprite = sprite;
        _renderer.flipX = Xflip;
    }
}
