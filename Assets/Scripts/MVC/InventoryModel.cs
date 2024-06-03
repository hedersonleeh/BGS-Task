using System;
using System.Collections;
using System.Collections.Generic;
namespace InventoryMVC
{
    public delegate void OnInventoryUpdate();
    public delegate void SwapEvent(ItemData newEquip, ItemData oldEquip);
    public class InventoryModel
    {
        public ItemData equippedHat { get; private set; }
        public ItemData equippedHair { get; private set; }
        public ItemData equippedBody { get; private set; }

        public Inventory inventory { get; private set; }
        public SwapEvent onEquip;
        public InventoryModel(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void OnItemSelect(ItemSlot item)
        {
            switch (item.data.type)
            {
                case ItemData.Type.HAT:
                    if (item.data.ID != equippedHat.ID)
                    {
                        onEquip?.Invoke(item.data, equippedHat);
                        equippedHat = item.data;
                    }

                    break;
                case ItemData.Type.CLOTHES:
                    if (item.data.ID != equippedBody.ID)
                    {
                        onEquip?.Invoke(item.data, equippedBody);
                        equippedBody = item.data;
                    }


                    break;
                case ItemData.Type.HAIR:
                    if (item.data.ID != equippedHair.ID)
                    {
                        onEquip?.Invoke(item.data, equippedHair);
                        equippedHair = item.data;
                    }
                    break;

            }
        }

        internal bool isEquipped(ItemData item)
        {
            return (item.ID == equippedBody.ID || item.ID == equippedHair.ID || item.ID == equippedHat.ID);
        }
    }

}
