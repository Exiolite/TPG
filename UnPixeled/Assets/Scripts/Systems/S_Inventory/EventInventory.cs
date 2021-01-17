using UnityEngine;
using UnityEngine.Events;

namespace Systems.S_Inventory
{
    [System.Serializable]
    public static class EventInventory
    {
        public static InventoryAction inventoryAction = new InventoryAction();
        public static InventoryUpdate inventoryUpdate = new InventoryUpdate();

        public static EquipmentAction equipmentAction = new EquipmentAction();
        public static EquipmentUpdate equipmentUpdate = new EquipmentUpdate();
    }
    public class InventoryAction : UnityEvent<Inventory.InventoryActions, ItemData, int> { }
    public class InventoryUpdate : UnityEvent { }
    
    public class EquipmentAction : UnityEvent<ItemData> { }
    public class EquipmentUpdate : UnityEvent { }
}