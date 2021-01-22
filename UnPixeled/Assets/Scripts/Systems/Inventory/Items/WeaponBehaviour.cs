using Core;
using UnityEngine;

namespace Systems.Inventory.Items
{
    public class WeaponBehaviour : MonoBehaviour
    {
        private bool _isAttacking;
        


        public void ItemSetInactive()
        {
            GetComponentInChildren<ItemBehaviour>().SetInactive();
        }

        private void Update()
        {
            if (GameManager.instance.playerBehaviour.PlayerAnimator.IsAttacking)
            {
                
            }
        }
    }
}