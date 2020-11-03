//Copyright Ex/IO 2020
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot //Реализация колличества предмета с помощью доп класса
{
    public ItemData item;
    public int count;

    public InventorySlot(ItemData _item)
    {
        item = _item;
        count = _item.count;
    }

    public void AddCount(int _value)
    {
        count += _value;
    }
}



[CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory System/Inventory", order = 51)]

public class Inventory : ScriptableObject
{
    public enum InventoryActions
    {
        add,
        remove
    }
    public InventoryActions inventoryAcctions;

    public List<InventorySlot> container = new List<InventorySlot>();//Массив предметов


    public void InventoryAction(InventoryActions _action, ItemData _item, int _count)
    {
        switch (_action)
        {
            case InventoryActions.add:
                AddItem(_item, _count);
                break;

            case InventoryActions.remove:
                RemoveItem(_item, _count);
                break;
        }
    }

    public void AddItem (ItemData _item, int _count)
    {
        for (int i = 0; i < container.Count; i++)
            if (container[i].item == _item && _item.stackable)
            {
                container[i].AddCount(_count);
                return;
            }

        container.Add(new InventorySlot(_item));
    }

    public void RemoveItem(ItemData _item, int _count)
    {
        for (int i = 0; i < container.Count; i++)
            if (container[i].item.itemName == _item.itemName)
                if (container[i].count > 1)
                    container[i].count -= _count;
                else
                    container.RemoveAt(i);
    }
}
