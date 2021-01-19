using UnityEngine.Events;

namespace Systems.S_Inventory
{
    [System.Serializable]
    public static class EventInventory
    {
        public static AddItem addItem = new AddItem();
        public static RemoveItem removeItem = new RemoveItem();

        public static UseItem useItem = new UseItem();
        public static UpdateInventory updateInventory = new UpdateInventory();
    }
    public class AddItem : UnityEvent<ItemData, int> { }
    public class RemoveItem : UnityEvent<ItemData, int> { }
    public class UseItem : UnityEvent<ItemData> { }
    public class UpdateInventory : UnityEvent { }

}