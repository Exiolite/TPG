using Systems.Inventory.Items;
using Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Systems.Inventory
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment", order = 0)]
    public class Equipment : ScriptableObject
    {
        private bool _isEquipped;
        
        private Item _weapon;
        public Item Weapon => _weapon;



        public void Clear()
        {
            _weapon = null;
        }
        
        public void UseItem([NotNull] Item item, bool isEquipped)
        {
            _isEquipped = isEquipped;
            if (item.GetType() == typeof(Weapon))
            {
                EquipmentActions(ref _weapon, item, GameManager.instance.playerBehaviour.WeaponHandler.transform);
            }
        }



        private void EquipmentActions(ref Item equipmentItem, Item item, Transform transform)
        {
            if (equipmentItem == null)
            {
                equipmentItem = item;
                EventInventory.removeItem.Invoke(item);
                Instantiate(item.prefab, transform).GetComponent<WeaponBehaviour>().ItemSetInactive();
            }
            else
            {
                if (equipmentItem.name == item.name && _isEquipped)
                {
                    EventInventory.addItem.Invoke(equipmentItem, equipmentItem.count);
                    equipmentItem = null;
                    EventInventory.updateInventory.Invoke();
                    for (var i = 0; i < transform.childCount; i++)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }
                else
                {
                    EquipmentActions(ref equipmentItem, item, transform);
                }
            }
        }
    }
}