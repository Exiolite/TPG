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
                    EventManager.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, _item.count);
                    weaponSlot = _item; //запись оружия в слот
                    Instantiate(weaponSlot.prefab, GameManager.instance.playerBehaviour.weaponHandler.transform);//создание оружия в руке
                    GameManager.instance.playerBehaviour.weaponCollider = GameManager.instance.playerBehaviour.weaponHandler.transform.GetChild(0).GetComponent<BoxCollider>();//установка коллайдера оружия
                    GameManager.instance.playerBehaviour.weaponCollider.enabled = false;//отключение коллайдера оружия

                }
                else
                {
                    if (weaponSlot.itemName == _item.itemName)
                        UpEqip(weaponSlot);
                    else
                    {
                        UpEqip(weaponSlot);
                        EventManager.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, _item.count);
                        weaponSlot = _item; //запись оружия в слот
                        Instantiate(_item.prefab, GameManager.instance.playerBehaviour.weaponHandler.transform);//создание оружия в руке
                        GameManager.instance.playerBehaviour.weaponCollider = GameManager.instance.playerBehaviour.weaponHandler.transform.GetChild(1).GetComponent<BoxCollider>();//установка коллайдера оружия
                        GameManager.instance.playerBehaviour.weaponCollider.enabled = false;//отключение коллайдера оружия
                    }
                }
                break;

            case ItemData.ItemType.armour:
                break;

            case ItemData.ItemType.consumable:
                if (GameManager.instance.playerBehaviour.GetComponent<HealthStatsPlayer>().health < GameManager.instance.playerBehaviour.GetComponent<HealthStatsPlayer>().healthMax)
                {
                    EventManager.playerStatAction.Invoke("add", "health", _item.restoreHealth);
                    EventManager.inventoryAction.Invoke(Inventory.InventoryActions.remove, _item, 1);
                }
                break;
        }
    }

    public void UpEqip(ItemData _item)
    {
        switch (_item.itemType)
        {
            case ItemData.ItemType.weapon:
                EventManager.inventoryAction.Invoke(Inventory.InventoryActions.add, _item, _item.count);
                Destroy(GameManager.instance.playerBehaviour.weaponHandler.transform.GetChild(0).gameObject);//уничтожение оружия в руке
                GameManager.instance.playerBehaviour.weaponCollider = null;//установка коллайдера в ноль
                weaponSlot = null;
                break;

            case ItemData.ItemType.armour:
                break;
        }
    }

    //SaveLoad
    public void Clear()
    {
        for (int i = 0; i < GameManager.instance.playerBehaviour.weaponHandler.transform.childCount; i++)
            Destroy(GameManager.instance.playerBehaviour.weaponHandler.transform.GetChild(i).gameObject);//уничтожение оружия в руке
        GameManager.instance.playerBehaviour.weaponCollider = null;//установка коллайдера в ноль
        weaponSlot = null;
    }
}
