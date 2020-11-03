//Copyright Ex/IO 2020
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData item;

    public GameObject spriteGObject;



    private void Awake()
    {
        InstancePickUpSprite();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            spriteGObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            spriteGObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void InstancePickUpSprite ()
    {
        spriteGObject = new GameObject("pickUpSprite");
        spriteGObject.transform.SetParent(transform);
        SpriteRenderer _curSprite = spriteGObject.AddComponent<SpriteRenderer>();
        spriteGObject.transform.rotation = new Quaternion(0, 90, 90, 1);
        spriteGObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        _curSprite.sprite = GameManager.instance.pickUpSprite;
        _curSprite.enabled = false;
    }
}
