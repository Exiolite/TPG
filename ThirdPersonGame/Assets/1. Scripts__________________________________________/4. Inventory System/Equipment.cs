//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

//[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory System/Equipment", order = 51)]
public class Equipment : ScriptableObject
{
    public enum EquipmentActions
    {
        equip,
        upequip
    }
    public EquipmentActions equipmentActions;

    [Header("EqupmentSlots")]
    public ItemData weaponSlot; //Слот для оружия



    public void EquipmentAction(ItemData _item)
    {
        switch (_item.itemType)
        {
            case ItemData.ItemType.weapon:
                if (weaponSlot == null)
                {
                    EventGame.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, _item.count);
                    weaponSlot = _item; //запись оружия в слот
                    Instantiate(weaponSlot.prefab, GameManager.instance.playerManager.rightHand.transform);//создание оружия в руке
                    GameManager.instance.playerManager.weaponCollider = GameManager.instance.playerManager.rightHand.transform.GetChild(0).GetComponent<BoxCollider>();//установка коллайдера оружия
                    GameManager.instance.playerManager.weaponCollider.enabled = false;//отключение коллайдера оружия

                }
                else
                {
                    if (weaponSlot.itemName == _item.itemName)
                        UpEqip(weaponSlot);
                    else
                    {
                        UpEqip(weaponSlot);
                        EventGame.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, _item.count);
                        weaponSlot = _item; //запись оружия в слот
                        Instantiate(_item.prefab, GameManager.instance.playerManager.rightHand.transform);//создание оружия в руке
                        GameManager.instance.playerManager.weaponCollider = GameManager.instance.playerManager.rightHand.transform.GetChild(1).GetComponent<BoxCollider>();//установка коллайдера оружия
                        GameManager.instance.playerManager.weaponCollider.enabled = false;//отключение коллайдера оружия
                    }
                }
                break;

            case ItemData.ItemType.armour:
                break;

            case ItemData.ItemType.consumable:
                if (GameManager.instance.playerManager.GetComponent<HealthStats_player>().health < GameManager.instance.playerManager.GetComponent<HealthStats_player>().healthMax)
                {
                    EventGame.playerStatAction.Invoke("add", "health", _item.restoreHealth);
                    EventGame.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, 1);
                }
                break;
        }
    }

    public void UpEqip(ItemData _item)
    {
        switch (_item.itemType)
        {
            case ItemData.ItemType.weapon:
                EventGame.inventoryAction.Invoke(Inventory.InventoryActions.add, _item, _item.count);
                Destroy(GameManager.instance.playerManager.rightHand.transform.GetChild(0).gameObject);//уничтожение оружия в руке
                GameManager.instance.playerManager.weaponCollider = null;//установка коллайдера в ноль
                weaponSlot = null;
                break;

            case ItemData.ItemType.armour:
                break;
        }
    }

    //SaveLoad
    public void Clear()
    {
        for (int i = 0; i < GameManager.instance.playerManager.rightHand.transform.childCount; i++)
            Destroy(GameManager.instance.playerManager.rightHand.transform.GetChild(i).gameObject);//уничтожение оружия в руке
        GameManager.instance.playerManager.weaponCollider = null;//установка коллайдера в ноль
        weaponSlot = null;
    }
}
