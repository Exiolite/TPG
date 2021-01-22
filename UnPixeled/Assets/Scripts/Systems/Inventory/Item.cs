using Systems.Inventory.Items;
using UnityEngine;

namespace Systems.Inventory
{
    public abstract class Item : ScriptableObject
    {
        public string itemName;
        public string ItemName => itemName;
        
        public string itemDescription;
        public string ItemDescription => itemDescription;
        
        public Sprite itemIcon;
        public Sprite ItemIcon => itemIcon;

        public Vector2 slotSize = new Vector2(1,1);
        public Vector2 SlotSize => slotSize;
        
        public GameObject prefab;
        public GameObject Prefab => prefab;
        
        public int count = 1;
        public int Count => count;
        
        public bool stackable;
        public bool Stackable => stackable;

        public int numberOfUses = 1;
        public int NumberOfUses => numberOfUses;
        

        public abstract void UseItem();

        public abstract Drink GetDrinkData();
        public abstract Weapon GetWeaponData();
        public abstract Heal GetHealData();
        public abstract Food GetFoodData();
    }
}