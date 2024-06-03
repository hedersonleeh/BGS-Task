using UnityEngine;

public class ViewBase : MonoBehaviour
{
    public delegate void OnItemHoverEvent(ItemSlot slot);
    public delegate void OnItemSelectEvent(ItemSlot slot);
    public OnItemHoverEvent hoverEvent;
    public OnItemSelectEvent selectEvent;
    public virtual void OnItemSelect(ItemSlot slot)
    {
        selectEvent?.Invoke(slot);
    }
    public virtual void OnItemHover(ItemSlot slot)
    {    
        hoverEvent?.Invoke(slot);
    }
}