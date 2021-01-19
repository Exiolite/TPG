namespace Systems.S_Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData item;
        public int count;

        public InventorySlot(ItemData item)
        {
            this.item = item;
            count = item.count;
        }

        public void AddCount(int value)
        {
            count += value;
        }
    }
}