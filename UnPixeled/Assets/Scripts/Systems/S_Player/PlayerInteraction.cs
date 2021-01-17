using Systems.S_Inventory;
using Core.Managers;
using UnityEngine;

namespace Systems.S_Player
{
    public class PlayerInteraction
    {
        public void Interact(Collider other)
        {
            if (other.gameObject.tag == "Item" && GameManager.instance.inputManager.Interact())
            {
                EventInventory.inventoryAction.Invoke(Inventory.InventoryActions.add, other.GetComponent<Item>().item,
                    other.GetComponent<Item>().item.count);
            }
            else if (other.gameObject.tag == "Interactive" && GameManager.instance.inputManager.Interact())
            {
                EventManager.playerIneraction.Invoke(other.gameObject);
            }
        }
    }
}