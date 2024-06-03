using System.Collections;
using System.Collections.Generic;
namespace InventoryMVC
{
    public class InventoryModel
    {
        private ItemData currentHat;
        private ItemData currentHair;
        private ItemData currentBody;

        private Inventory _inventory;

        public delegate void OnClothesUpdate();
    }
    
}
