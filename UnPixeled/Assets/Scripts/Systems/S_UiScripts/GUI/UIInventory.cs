using System.Collections.Generic;
using Systems.S_Inventory;
using Core.Managers.Game;
using UnityEngine;

namespace Systems.S_UiScripts.GUI
{
    public class UiInventory : MonoBehaviour
    {
        public GameObject uiItemGrid;
        public GameObject uiItem;
        public GameObject uiWeaponSlot;
        
        void Awake()
        {
            EventInventory.updateInventory.AddListener(UpdateInventory);
        }
        
        void UpdateInventory()
        {
            List<InventorySlot> container = GameManager.instance.playerBehaviour.Inventory.InventoryContainer.container;
            for (int i = 0; i < uiItemGrid.transform.childCount; i++)
            {
                Destroy(uiItemGrid.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < container.Count; i++)
            {
                GameObject uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
                uiItemSlot.GetComponent<UiItemSlot>().SetSlot(container[i].item, container[i].count);
            }
        }
    }
}
