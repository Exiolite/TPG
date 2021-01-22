using Core;
using UnityEngine;

namespace Systems.Inventory
{
    public class ItemBehaviour : MonoBehaviour
    {
        public Item item;

        private bool _inRangeOfPlayer;



        public void SetInactive()
        {
            gameObject.SetActive(false);
        }
        
        private void Awake()
        {
            EventInput.interact.AddListener(PickUpItem);
        }

        private void PickUpItem()
        {
            if (!_inRangeOfPlayer) return;
            EventInventory.addItem.Invoke(item, item.count);
            Destroy(transform.parent.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _inRangeOfPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _inRangeOfPlayer = false;
        }
    }
}