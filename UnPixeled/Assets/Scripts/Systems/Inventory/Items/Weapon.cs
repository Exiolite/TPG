using System;
using UnityEngine;

namespace Systems.Inventory.Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Inventory System/Items/Weapon", order = 51)]
    public class Weapon : Item
    {
        [Header("Main")]
        public float damage;
        

        public override void UseItem()
        {
            EventInventory.useItem.Invoke(this);
        }

        public override Drink GetDrinkData()
        {
            throw new NotImplementedException();
        }

        public override Weapon GetWeaponData()
        {
            return this;
        }

        public override Heal GetHealData()
        {
            throw new NotImplementedException();
        }

        public override Food GetFoodData()
        {
            throw new NotImplementedException();
        }
    }
}