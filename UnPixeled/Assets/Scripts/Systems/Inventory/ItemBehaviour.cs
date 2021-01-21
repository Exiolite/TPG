using Core;
using UnityEngine;

namespace Systems.Inventory
{
    public class ItemBehaviour : MonoBehaviour
    {
        public Item item;
        
        
        
        private void Awake()
        {
            EventInput.interact.AddListener(PickUpItem);
        }

        private void PickUpItem()
        {
            EventInventory.addItem.Invoke(item, item.count);
            Destroy(gameObject);
        }
    }
}