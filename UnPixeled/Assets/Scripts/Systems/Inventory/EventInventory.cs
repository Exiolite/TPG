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
        public static ClearInventory clearInventory = new ClearInventory();
    }
    public class AddItem : UnityEvent<Item, int> { }
    public class RemoveItem : UnityEvent<Item> { }
    public class UseItem : UnityEvent <Item,bool> { }
    public class UpdateInventory : UnityEvent { }
    public class ClearInventory : UnityEvent { }
}