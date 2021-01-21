using Systems.Inventory;
using Core;
using UnityEngine;

namespace Systems.Ui.Gui
{
    public class InventoryUiBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject uiItemGrid;
        [SerializeField] private GameObject uiItem;
        [SerializeField] private GameObject uiWeaponSlot;

        
        
        private void Awake()
        {
            EventInventory.updateInventory.AddListener(UpdateInventory);
        }

        private void UpdateInventory()
        {
            var container = GameManager.instance.playerBehaviour.Inventory.container;
            for (var i = 0; i < uiItemGrid.transform.childCount; i++)
            {
                Destroy(uiItemGrid.transform.GetChild(i).gameObject);
            }

            foreach (var t in container)
            {
                GameObject uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
                uiItemSlot.GetComponent<ItemSlotUiBehaviour>().SetSlot(t.item, t.count);
            }
        }
    }
}