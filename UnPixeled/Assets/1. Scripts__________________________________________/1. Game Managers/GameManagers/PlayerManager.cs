//Copyright Ex/IO 2020
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Speed")]
    [SerializeField] public float playerSpeed; //the speed of players run
    [SerializeField] public float dashSpeed; //the speed of players dash
    float speed;


    [Header("Components")]
    Animator playerAnim;
    public CharacterController charController;
    public GameObject playerModel; //children object with player character for rotation
    public BoxCollider weaponCollider; //Box collider on weapon for collizion damage to enemies
    public GameObject rightHand;
    public Inventory playerInventory;
    public Equipment playerEquipment;

    [Header("Animation states")]
    public bool defenceState = false; //State of players attack ot defence



    public void Awake()
    {
        EventGame.playerDead.AddListener(ResetPlayer);
        EventGame.inventoryAction.AddListener(PlayerInventoryAction);
        EventGame.equipmentAction.AddListener(PlayerEqupmentAction);
    }



    private void Start()
    {
        speed = playerSpeed;

        //initialize components
        charController = GetComponent<CharacterController>();
        if (weaponCollider != null)
            weaponCollider.enabled = false;// reset weapon collider


        //initialize player model
        playerAnim = playerModel.GetComponent<Animator>();
    }



    private void OnTriggerStay(Collider other)
    {
            if (other.gameObject.tag == "Item" && GameManager.instance.inputManager.Interact())
            {
                EventGame.inventoryAction.Invoke(Inventory.InventoryActions.add, other.GetComponent<Item>().item, other.GetComponent<Item>().item.count);
                EventAudio.pickUp.Invoke(GetComponent<AudioSource>());
                Destroy(other.gameObject);
            }
            else if (other.gameObject.tag == "Interactive" && GameManager.instance.inputManager.Interact())
            {
                EventGame.playerIneraction.Invoke(other.gameObject);
            }
    }



    void Update()
    {
        charController.Move(GameManager.instance.inputManager.WASDMovement() * speed);

        PlayerDash();
        AnimationPlayer();
    }



    private void OnApplicationQuit()
    {
        playerInventory.container.Clear();
        playerEquipment.Clear();
    }



    void AnimationPlayer()
    {
        if (weaponCollider != null)
        {
            AttackAnimation();
            DefenceAnimation();
        }
        MovementAnimations();
    }

    void PlayerRotate()
    {
        float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        playerModel.transform.rotation = Quaternion.Euler(new Vector3(0, angle + 45, 0));
    }

    void PlayerDash()
    {
        if (GameManager.instance.inputManager.Dash())
        {
            EventGame.playerStatAction.Invoke("remove", "energy", GetComponent<HealthStats_player>().energyUseForDash);
            speed = dashSpeed;
        }
        else
            speed = playerSpeed;
    }

    void MovementAnimations()
    {
        if (GameManager.instance.inputManager.inputWASD)
        {
            PlayerRotate();
            playerAnim.SetInteger("move", 1);
        }
        else
            playerAnim.SetInteger("move", 0);
    }

    void AttackAnimation()
    {
        if (GameManager.instance.inputManager.LeftMouseButton())
        {
            weaponCollider.enabled = true;
            playerAnim.SetInteger("Attack", 1);

            switch (playerEquipment.weaponSlot.weaponType)
            {
                case ItemData.WeaponType.stuff:
                    playerAnim.SetInteger("stuffAttack", 1);
                    break;

                case ItemData.WeaponType.sword:
                    playerAnim.SetInteger("swordAttack", 1);
                    break;
            }
        }
        else if (Input.GetAxis("Attack") == 0)
        {
            playerAnim.SetInteger("stuffAttack", 0);
            playerAnim.SetInteger("swordAttack", 0);
            playerAnim.SetInteger("Attack", 0);
            weaponCollider.enabled = false;
        }
        else
        {
            playerAnim.SetInteger("stuffAttack", 0);
            playerAnim.SetInteger("swordAttack", 0);
            playerAnim.SetInteger("Attack", 0);
        }
    }

    void DefenceAnimation()
    {
        if (GameManager.instance.inputManager.RightMouseButton() && GetComponent<HealthStats_player>().energy > 0)
        {
            weaponCollider.enabled = true;
            defenceState = true;
            EventGame.playerStatAction.Invoke("remove", "energy", GetComponent<HealthStats_player>().energyUsageForDefence);
            switch (playerEquipment.weaponSlot.weaponType)
            {
                case ItemData.WeaponType.stuff:
                    playerAnim.SetInteger("stuffDefence", 1);
                    break;

                case ItemData.WeaponType.sword:
                    playerAnim.SetInteger("swordDefence", 1);
                    break;
            }
        }
        else
        {
            playerAnim.SetInteger("stuffDefence", 0);
            playerAnim.SetInteger("swordDefence", 0);
            defenceState = false;
        }
    }


    //InventoryActions
    void PlayerInventoryAction(Inventory.InventoryActions _action, ItemData _item, int _count)
    {
        playerInventory.InventoryAction(_action, _item, _count);
        EventGame.inventoryUpdate.Invoke();
    }

    void PlayerEqupmentAction(ItemData _item)
    {
        playerEquipment.EquipmentAction(_item);
        EventGame.equipmentUpdate.Invoke();
    }



    //Save/Load
    public void ResetPlayer()
    {
        playerEquipment.Clear();
        playerInventory.container.Clear();
        EventGame.inventoryUpdate.Invoke();
        EventGame.equipmentUpdate.Invoke();
    }
}