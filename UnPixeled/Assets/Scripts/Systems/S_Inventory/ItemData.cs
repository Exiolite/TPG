//Copyright Ex/IO 2020

using UnityEngine;

namespace Systems.S_Inventory
{
    [CreateAssetMenu(fileName = "New ItemData", menuName = "Inventory System/Item", order = 51)]
    public class ItemData : ScriptableObject
    {
        public GameObject prefab;
        public Sprite itemIcon;
        public string itemName;
        public bool stackable;
        public int count;
        public enum ItemType
        {
            etc,
            weapon,
            armour,
            consumable
        }
        public ItemType itemType;

        [Header("Weapon")]
        public float weaponDamage;
        public float critChance;
        public enum WeaponType
        {
            stuff,
            sword
        }
        public WeaponType weaponType;

        [Header("Armour")]

        [Header("Counsumable")]
        public float restoreHealth;



        public Sprite ItemIcon
        {
            get
            {
                return itemIcon;
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }
        }

        public bool Stackable
        {
            get
            {
                return stackable;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public GameObject Prefab
        {
            get
            {
                return prefab;
            }
        }

        public float WeaponDamage
        {
            get
            {
                return weaponDamage;
            }
        }

        public float CritChance
        {
            get
            {
                return critChance;
            }
        }

        public float RestoreHealth
        {
            get
            {
                return restoreHealth;
            }
        }
    }
}
