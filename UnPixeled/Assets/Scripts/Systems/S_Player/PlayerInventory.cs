using Systems.S_Inventory;
using Core.Managers;

namespace Systems.S_Player
{
    public class PlayerInventory
    {
        private Inventory _playerInventory;
        private Equipment _playerEquipment;

        public void InitializeComponent(Inventory playerInventory, Equipment playerEquipment)
        {
            _playerEquipment = playerEquipment;
            _playerInventory = playerInventory;
        }
        
        public void PlayerInventoryAction(Inventory.InventoryActions action, ItemData item, int count)
        {
            _playerInventory.InventoryAction(action, item, count);
            EventInventory.inventoryUpdate.Invoke();
        }

        public void PlayerEqupmentAction(ItemData item)
        {
            _playerEquipment.EquipmentAction(item);
            EventInventory.equipmentUpdate.Invoke();
        }
        
        //Save/Load
        public void ResetPlayer()
        {
            _playerEquipment.Clear();
            _playerInventory.container.Clear();
            EventInventory.inventoryUpdate.Invoke();
            EventInventory.equipmentUpdate.Invoke();
        }
    }
}