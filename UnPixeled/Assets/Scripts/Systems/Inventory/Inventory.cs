using System;
using System.Collections.Generic;
using Systems.Inventory.Items;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory", order = 51)]
    public class Inventory : ScriptableObject
    {
        public List<InventorySlot> container = new List<InventorySlot>();
        
        public void Clear()
        {
            container.Clear();
        }
        
        public void AddItem(Item item, int count)
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item && item.stackable)
                {
                    container[i].AddCount(count);
                    EventInventory.updateInventory.Invoke();
                    return;
                }
            }

            container.Add(new InventorySlot(item));
            EventInventory.updateInventory.Invoke();
        }

        public void RemoveItem([NotNull] Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            
            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item.itemName == item.itemName)
                {
                    if (container[i].count > 1)
                    {
                        container[i].count -= 1;
                        EventInventory.updateInventory.Invoke();
                    }
                    else
                    {
                        container.RemoveAt(i);
                        EventInventory.updateInventory.Invoke();
                    }
                }
            }
        }
    }
}