using Systems.Inventory;
using Core;
using UnityEngine;

namespace Systems.Ui.Gui
{
    public class InventoryUiBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject uiItemGrid;
        [SerializeField] private GameObject uiWeaponSlot;
        
        [SerializeField] private ItemSlotUiBehaviour uiItem;

        
        
        private void Awake()
        {
            EventInventory.updateInventory.AddListener(UpdateInventory);
        }

        private void UpdateInventory()
        {
            var playerBehaviour = GameManager.instance.playerBehaviour;
            for (var i = 0; i < uiItemGrid.transform.childCount; i++)
            {
                Destroy(uiItemGrid.transform.GetChild(i).gameObject);
            }

            for (int j = 0; j < playerBehaviour.Inventory.container.Count; j++)
            {
                var uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
                uiItemSlot.SetSlot(playerBehaviour.Inventory.container[j], j);
            }

            if (playerBehaviour.Equipment.weapon != null)
            { 
                var uiItemSlot = Instantiate(uiItem, uiWeaponSlot.transform);
                uiItemSlot.SetSlot(new InventorySlot(playerBehaviour.Equipment.weapon), 1);
            }
        }
    }
}