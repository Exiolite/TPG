using Systems.S_Inventory;
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
            EventInventory.inventoryUpdate.AddListener(UpdateInventory);
            EventInventory.equipmentUpdate.AddListener(UpdateEquipment);
        }



        void UpdateInventory()
        {
            for (int i = 0; i < uiItemGrid.transform.childCount; i++)
                Destroy(uiItemGrid.transform.GetChild(i).gameObject);

            for (int i = 0; i < GameManager.instance.playerBehaviour.PlayerInventory.container.Count; i++)
            {
                GameObject uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
                uiItemSlot.GetComponent<UiItemSlot>().SetSlot(GameManager.instance.playerBehaviour.PlayerInventory.container[i].item, GameManager.instance.playerBehaviour.PlayerInventory.container[i].count);
            }
        }

        void UpdateEquipment()
        {
            if (GameManager.instance.playerBehaviour.PlayerEquipment.weaponSlot != null)
            {
                for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                    Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);

                GameObject weaponSlot = Instantiate(uiItem, uiWeaponSlot.transform);
                weaponSlot.GetComponent<UiItemSlot>().SetSlot(GameManager.instance.playerBehaviour.PlayerEquipment.weaponSlot, GameManager.instance.playerBehaviour.PlayerEquipment.weaponSlot.count);
            }
            else
                for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                    Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);
        }
    }
}
