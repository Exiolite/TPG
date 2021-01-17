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
        EventManager.inventoryUpdate.AddListener(UpdateInventory);
        EventManager.equipmentUpdate.AddListener(UpdateEquipment);
    }



    void UpdateInventory()
    {
        for (int i = 0; i < uiItemGrid.transform.childCount; i++)
            Destroy(uiItemGrid.transform.GetChild(i).gameObject);

        for (int i = 0; i < GameManager.instance.playerBehaviour.playerInventory.container.Count; i++)
        {
            GameObject uiItemSlot = Instantiate(uiItem, uiItemGrid.transform);
            uiItemSlot.GetComponent<UIItemSlot>().SetSlot(GameManager.instance.playerBehaviour.playerInventory.container[i].item, GameManager.instance.playerBehaviour.playerInventory.container[i].count);
        }
    }

    void UpdateEquipment()
    {
        if (GameManager.instance.playerBehaviour.playerEquipment.weaponSlot != null)
        {
            for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);

            GameObject weaponSlot = Instantiate(uiItem, uiWeaponSlot.transform);
            weaponSlot.GetComponent<UIItemSlot>().SetSlot(GameManager.instance.playerBehaviour.playerEquipment.weaponSlot, GameManager.instance.playerBehaviour.playerEquipment.weaponSlot.count);
        }
        else
            for (int i = 0; i < uiWeaponSlot.transform.childCount; i++)
                Destroy(uiWeaponSlot.transform.GetChild(i).gameObject);
    }
}
