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
        private bool _isEquipped;
        
        
        public void SetSlot(InventorySlot inventorySlot, int itemIndex, bool isEquipped)
        {
            _inventorySlot = inventorySlot;
            textName.GetComponent<Text>().text = inventorySlot.item.itemName;
            textCount.GetComponent<Text>().text = inventorySlot.count > 1 ? inventorySlot.count.ToString() : "";
            GetComponent<Image>().sprite = inventorySlot.item.itemIcon;
            _isEquipped = isEquipped;
        }
        
        public void OnClicked()
        {
            _inventorySlot.item.UseItem(_isEquipped);
        }

        public void DestroySlot()
        {
            Destroy(this);
        }
    }
}