namespace Systems.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;
        public int count;

        public InventorySlot(Item _item)
        {
            this.item = _item;
            count = item.count;
        }

        public void AddCount(int value)
        {
            count += value;
        }
    }
}