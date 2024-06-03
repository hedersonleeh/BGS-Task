using System;
using System.Collections;
using System.Collections.Generic;
namespace InventoryMVC
{
    public delegate void OnInventoryUpdate();
    public delegate void SwapEvent(ItemData newEquip);
    public delegate void UnEquipEvent(ItemData equip);
    public class InventoryModel
    {
        public static ItemData equippedHat { get; private set; }
        public static ItemData equippedHair { get; private set; }
        public static ItemData equippedBody { get; private set; }

        public Inventory inventory { get; private set; }
        public SwapEvent onEquip;
        public UnEquipEvent unEquip;
        public InventoryModel(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void OnItemSelect(ItemSlot item)
        {
            if (isEquipped(item.data))
            {
                item.Clear();
                switch (item.data.type)
                {
                    case ItemData.Type.HAT:
                        equippedHat = default;
                        break;
                    case ItemData.Type.CLOTHES:
                        equippedBody = default;
                        break;
                    case ItemData.Type.HAIR:
                        equippedHair = default;
                        break;                   
                }
                unEquip?.Invoke(item.data);
            }
            else
                switch (item.data.type)
                {
                    case ItemData.Type.HAT:
                        if (item.data.ID != equippedHat.ID)
                        {
                            equippedHat = item.data;
                            onEquip?.Invoke(item.data);
                        }
                        break;
                    case ItemData.Type.CLOTHES:
                        if (item.data.ID != equippedBody.ID)
                        {
                            equippedBody = item.data;
                            onEquip?.Invoke(item.data);
                        }


                        break;
                    case ItemData.Type.HAIR:
                        if (item.data.ID != equippedHair.ID)
                        {
                            equippedHair = item.data;
                            onEquip?.Invoke(item.data);
                        }
                        break;

                }
        }

        public static bool isEquipped(ItemData item)
        {
            return item.ID == equippedHat.ID || item.ID == equippedBody.ID || item.ID == equippedHair.ID;
        }
    }

}
