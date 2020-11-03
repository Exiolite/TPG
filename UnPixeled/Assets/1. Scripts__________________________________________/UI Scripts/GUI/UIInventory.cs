//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public GameObject uiItemGrid;
    public GameObject uiItem;
    public GameObject uiWeaponSlot;



    void Awake()
    {
        EventGame.inventoryUpdate.AddListener(UpdateInventory);
        EventGame.equipmentUpdate.AddListener(UpdateEquipment);
    }



    void UpdateInventory()
    {
        for (int i = 0; i < uiItemGrid.transform.childCount; i++)
            Destroy(uiItemGrid.transform.GetChild(i).gameObject);

        for (int i = 0; i < GameManager.instance.playerManager.playerInventory.container.Count; i++)
        {
            GameObject uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
            uiItemSlot.GetComponent<UIItemSlot>().SetSlot(GameManager.instance.playerManager.playerInventory.container[i].item, GameManager.instance.playerManager.playerInventory.container[i].count);
        }
    }

    void UpdateEquipment()
    {
        if (GameManager.instance.playerManager.playerEquipment.weaponSlot != null)
        {
            for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);

            GameObject weaponSlot = Instantiate(uiItem, uiWeaponSlot.transform);
            weaponSlot.GetComponent<UIItemSlot>().SetSlot(GameManager.instance.playerManager.playerEquipment.weaponSlot, GameManager.instance.playerManager.playerEquipment.weaponSlot.count);
        }
        else
            for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);
    }
}
