using UnityEngine;

namespace Systems.Inventory.Items
{
    [CreateAssetMenu(fileName = "Drink", menuName = "Inventory System/Items/Drink", order = 53)]
    public class Drink : Item
    {
        public float thirstRestore;
        public float effectDuration;
        
        
        
        public override void UseItem()
        {
            
        }

        public override Drink GetDrinkData()
        {
            return this;
        }

        public override Weapon GetWeaponData()
        {
            throw new System.NotImplementedException();
        }

        public override Heal GetHealData()
        {
            throw new System.NotImplementedException();
        }

        public override Food GetFoodData()
        {
            throw new System.NotImplementedException();
        }
    }
}