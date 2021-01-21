using Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Ui.Gui
{
    public class ItemSlotUiBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject textName;
        [SerializeField] private GameObject textCount;

        private Item _item;

        
        
        public void SetSlot(Item item, int count)
        {
            _item = item;
            textName.GetComponent<Text>().text = item.itemName;
            textCount.GetComponent<Text>().text = count > 1 ? count.ToString() : "";
            GetComponent<Image>().sprite = item.itemIcon;
        }
        
        public void OnClicked()
        {
            if (_item != null) EventInventory.useItem.Invoke(_item);
        }
        
        
        
        private void Awake()
        {
            _item = null;
        }
    }
}