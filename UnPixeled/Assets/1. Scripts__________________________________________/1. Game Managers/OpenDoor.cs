//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;

    public GameObject localSprite;
    public GameObject localSprite2;



    private void Awake()
    {
        EventGame.playerIneraction.AddListener(PlayAnimation);
        localSprite.GetComponent<SpriteRenderer>().enabled = false;
        localSprite2.GetComponent<SpriteRenderer>().enabled = false;
    }

    void PlayAnimation (GameObject _object)
    {
        if (this.gameObject == _object)
        {
            door.GetComponent<Animation>().Play();
            if (door.GetComponent<Animation>().clip != null)
                EventAudio.doorOpens.Invoke(GetComponent<AudioSource>());
            localSprite.GetComponent<SpriteRenderer>().enabled = false;
            localSprite2.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<Animation>().clip = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && door.GetComponent<Animation>().clip != null)
        {
            localSprite.GetComponent<SpriteRenderer>().enabled = true;
            localSprite2.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && door.GetComponent<Animation>().clip != null)
        {
            localSprite.GetComponent<SpriteRenderer>().enabled = false;
            localSprite2.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
