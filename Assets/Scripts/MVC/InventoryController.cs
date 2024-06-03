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
            _model.onEquip += OnEquip;
            _inventoryView.FillItems(_model.inventory.GetItems());

        }
        ~InventoryController()
        {
            _model.onEquip -= OnEquip;
        }
        private void OnEquip(ItemData newEquip, ItemData oldEquip)
        {
            _playerClothes.Equip(newEquip);
        }
    }
}