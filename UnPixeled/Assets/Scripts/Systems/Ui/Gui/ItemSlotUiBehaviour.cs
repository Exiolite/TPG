using Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Ui.Gui
{
    public class ItemSlotUiBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject textName;
        [SerializeField] private GameObject textCount;

        private InventorySlot _inventorySlot;

        
        
        public void SetSlot(InventorySlot inventorySlot, int itemIndex)
        {
            _inventorySlot = inventorySlot;
            textName.GetComponent<Text>().text = inventorySlot.item.itemName;
            textCount.GetComponent<Text>().text = inventorySlot.count > 1 ? inventorySlot.count.ToString() : "";
            GetComponent<Image>().sprite = inventorySlot.item.itemIcon;
        }
        
        public void OnClicked()
        {
            EventInventory.useItem.Invoke(_inventorySlot.item);
            Destroy(gameObject);
        }
    }
}