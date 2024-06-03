using UnityEngine;

public class PlayerClothes : MonoBehaviour
{
    [SerializeField] MultiLayerCharacterRenderer _body;
    [SerializeField] MultiLayerCharacterRenderer _hair;
    [SerializeField] MultiLayerCharacterRenderer _hat;

    public void Equip(ItemData item)
    {
        switch (item.type)
        {
            case ItemData.Type.HAT:
                _hat.AssingAtlas(item.GetAtlas());
                _hat.Tint(item.tint);
                break;
            case ItemData.Type.CLOTHES:
                _body.AssingAtlas(item.GetAtlas());
                _body.Tint(item.tint);
                break;
            case ItemData.Type.HAIR:
                _hair.AssingAtlas(item.GetAtlas());
                _hair.Tint(item.tint);
                break;
        }
    }
    public void UnEquip(ItemData.Type type)
    {
        switch (type)
        {
            case ItemData.Type.HAT:
                _hat.Clear();
                break;
            case ItemData.Type.CLOTHES:
                _body.Clear();
                break;
            case ItemData.Type.HAIR:
                _hair.Clear();
                break;

        }
    }
}
