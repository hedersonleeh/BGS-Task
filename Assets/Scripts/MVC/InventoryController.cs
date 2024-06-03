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

        }
        ~InventoryController()
        {
            _model.onEquip -= OnEquip;
        }
        public void OnOpenInventory()
        {
            _inventoryView.FillItems(_model.inventory.GetItems());
        }
        private void OnEquip(ItemData newEquip, ItemData oldEquip)
        {
            _playerClothes.Equip(newEquip);
        }
    }
}