using System;
using UnityEngine;

namespace Systems.Inventory.Items
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Inventory System/Items/Heal", order = 55)]
    public class Heal : Item
    {
        public float healthRestore;
        
        
        
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
            return this;
        }

        public override Food GetFoodData()
        {
            throw new NotImplementedException();
        }
    }
}