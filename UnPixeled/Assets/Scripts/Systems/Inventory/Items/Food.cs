using System;
using UnityEngine;

namespace Systems.Inventory.Items
{
    [CreateAssetMenu(fileName = "Food", menuName = "Inventory System/Items/Food", order = 54)]
    public class Food : Item
    {
        public float hungerRestore;
        
        
        
        public override void UseItem()
        {

        }

        public override Drink GetDrinkData()
        {
            throw new NotImplementedException();
        }

        public override Weapon GetWeaponData()
        {
            throw new NotImplementedException();
        }

        public override Heal GetHealData()
        {
            throw new NotImplementedException();
        }

        public override Food GetFoodData()
        {
            return this;
        }
    }
}