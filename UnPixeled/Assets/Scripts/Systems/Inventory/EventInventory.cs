using UnityEngine.Events;

namespace Systems.Inventory
{
    [System.Serializable]
    public static class EventInventory
    {
        public static AddItem addItem = new AddItem();
        public static RemoveItem removeItem = new RemoveItem();
        public static UseItem useItem = new UseItem();
        public static UpdateInventory updateInventory = new UpdateInventory();
    }
    public class AddItem : UnityEvent<Item, int> { }
    public class RemoveItem : UnityEvent<Item, int> { }
    public class UseItem : UnityEvent<Item> { }
    public class UpdateInventory : UnityEvent { }
}