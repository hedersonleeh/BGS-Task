using System;

namespace InventoryMVC
{
    public class InventoryController
    {
        private InventoryView _inventoryView { get; }
        private InventoryModel _model { get; }
        private PlayerClothes _playerClothes;
        public InventoryController(InventoryView inventoryView, InventoryModel model, PlayerClothes playerClothes)
        {
            _model = model;
            _inventoryView = inventoryView;
            _playerClothes = playerClothes;
            _model.onEquip += OnEquip;
            _model.unEquip += OnUnEquip;

        }
        ~InventoryController()
        {
            _model.onEquip -= OnEquip;
            _model.unEquip -= OnUnEquip;

        }
        public void OnOpenInventory()
        {
            _inventoryView.FillItems(_model.inventory.GetItems());
        }
        private void OnEquip(ItemData newEquip)
        {
            _playerClothes.Equip(newEquip);
        }
        private void OnUnEquip(ItemData item)
        {
            _playerClothes.UnEquip(item.type);
        }
    }
}