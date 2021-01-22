using System;
using Systems.Inventory.Items;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.Inventory
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment", order = 0)]
    public class Equipment : ScriptableObject
    {
        public Item weapon; //TODO: change public to private pls



        public void Clear()
        {
            weapon = null;
        }
        
        public void UseItem([NotNull] Item item)
        {

            if (item.GetType() == typeof(Weapon))
            {
                AddItem(ref weapon, item);
            }
            
            if (item.GetType() == typeof(Heal))
            {
                
            }
            
            if (item.GetType() == typeof(Food))
            {
                
            }
            
            if (item.GetType() == typeof(Drink))
            {
                
            }
        }



        private void AddItem(ref Item equipmentItem, Item item)
        {
            if (equipmentItem == null)
            {
                equipmentItem = item;
                EventInventory.removeItem.Invoke(item);
            }
            else if (equipmentItem == item)
            {
                EventInventory.addItem.Invoke(equipmentItem, equipmentItem.count);
                equipmentItem = null;
                equipmentItem = item;
            }
            else
            {
                EventInventory.addItem.Invoke(equipmentItem, equipmentItem.count);
                equipmentItem = null;
            }
        }
    }
}