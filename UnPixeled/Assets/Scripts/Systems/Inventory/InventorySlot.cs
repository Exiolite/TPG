namespace Systems.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;
        public int count;

        public InventorySlot(Item item)
        {
            this.item = item;
            count = this.item.count;
        }

        public void AddCount(int value)
        {
            count += value;
        }
    }
}