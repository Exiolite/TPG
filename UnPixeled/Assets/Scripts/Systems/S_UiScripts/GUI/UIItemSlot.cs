using Systems.S_Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.S_UiScripts.GUI
{
    public class UiItemSlot : MonoBehaviour
    {
        private ItemData _item;
        private GameObject _textName;
        private GameObject _textCount;



        public void OnClicked()
        {
            if (_item != null)
                EventInventory
                    .equipmentAction.Invoke(_item);
        }

        public void SetSlot(ItemData item, int count)
        {
            item = item;
            _textName.GetComponent<Text>().text = item.itemName;

            if (count > 1)
                _textCount.GetComponent<Text>().text = count.ToString();
            else
                _textCount.GetComponent<Text>().text = "";

            GetComponent<Image>().sprite = item.itemIcon;
        }
    }
}
