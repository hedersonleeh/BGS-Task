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
                break;
            case ItemData.Type.CLOTHES:
                _body.AssingAtlas(item.GetAtlas());
                break;
            case ItemData.Type.HAIR:
                _hair.AssingAtlas(item.GetAtlas());
                break;
        }
    }
}
