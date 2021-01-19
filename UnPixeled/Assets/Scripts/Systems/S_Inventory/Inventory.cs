using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.S_Inventory
{
    public class Inventory
    {
        private InventoryContainer _inventoryContainer;
        public InventoryContainer InventoryContainer => _inventoryContainer;

        
        public void SetContainer(InventoryContainer inventoryContainer)
        {
            _inventoryContainer = inventoryContainer;
        }

        public void ResetContainer()
        {
            _inventoryContainer.container.Clear();
        }
        
        public void AddItem(ItemData item, int count)
        {
            for (int i = 0; i < _inventoryContainer.container.Count; i++)
            {
                if (_inventoryContainer.container[i].item == item && item.stackable)
                {
                    _inventoryContainer.container[i].AddCount(count);
                    EventInventory.updateInventory.Invoke();
                    return;
                }
            }

            _inventoryContainer.container.Add(new InventorySlot(item));
            EventInventory.updateInventory.Invoke();
        }

        public void RemoveItem([NotNull] ItemData item, int count)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            
            for (int i = 0; i < _inventoryContainer.container.Count; i++)
            {
                if (_inventoryContainer.container[i].item.itemName == item.itemName)
                {
                    if (_inventoryContainer.container[i].count > 1)
                    {
                        _inventoryContainer.container[i].count -= count;
                        EventInventory.updateInventory.Invoke();
                    }
                    else
                    {
                        _inventoryContainer.container.RemoveAt(i);
                        EventInventory.updateInventory.Invoke();
                    }
                }
            }
        }

        public void UseItem([NotNull] ItemData item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            //TODO: использование предмета 
            EventInventory.updateInventory.Invoke();
        }
    }
}