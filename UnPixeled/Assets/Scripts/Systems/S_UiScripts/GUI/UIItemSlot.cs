using System;
using Systems.S_Inventory;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.S_UiScripts.GUI
{
    public class UiItemSlot : MonoBehaviour
    {
        private ItemData _item;
        
        [SerializeField] private GameObject textName;
        [SerializeField] private GameObject textCount;

        private void Awake()
        {
            _item = null;
        }

        public void OnClicked()
        {
            if (_item != null) EventInventory.useItem.Invoke(_item);
        }

        public void SetSlot([NotNull] ItemData item, int count)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _item = item;
            textName.GetComponent<Text>().text = item.itemName;

            if (count > 1)
                textCount.GetComponent<Text>().text = count.ToString();
            else
                textCount.GetComponent<Text>().text = "";

            GetComponent<Image>().sprite = item.itemIcon;
        }
    }
}
