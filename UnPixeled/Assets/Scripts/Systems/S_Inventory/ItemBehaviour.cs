//Copyright Ex/IO 2020

using System;
using Core.Managers.Game;
using Core.Managers.Input;
using UnityEngine;

namespace Systems.S_Inventory
{
    public class ItemBehaviour : MonoBehaviour
    {
        private bool _inRangeOfPlayer;
        public ItemData item;

        public GameObject spriteGObject;

        private void Awake()
        {
            _inRangeOfPlayer = false;
            InstancePickUpSprite();
            EventInput.interact.AddListener(PickUpItem);
        }

        private void PickUpItem()
        {
            if (_inRangeOfPlayer)
            {
                EventInventory.addItem.Invoke(item, item.count);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                spriteGObject.GetComponent<SpriteRenderer>().enabled = true;
                _inRangeOfPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                spriteGObject.GetComponent<SpriteRenderer>().enabled = false;
                _inRangeOfPlayer = false;
            }
        }

        void InstancePickUpSprite()
        {
            spriteGObject = new GameObject("pickUpSprite");
            spriteGObject.transform.SetParent(transform);
            spriteGObject.AddComponent<SpriteRenderer>();
            spriteGObject.transform.rotation = new Quaternion(0, 90, 90, 1);
            spriteGObject.transform.position =
                new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            spriteGObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.pickUpSprite;
            spriteGObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}