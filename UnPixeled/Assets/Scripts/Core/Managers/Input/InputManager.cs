//Copyright Ex/IO 2020

using System.Collections;
using System.Collections.Generic;
using Core.Managers.Input;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float moveX, moveZ; // WASD

    public bool inputWASD;
    public bool disableMovement;
    public bool disableDash;

    public bool disableMouse = false;
    public bool disableDefence = false;

    public bool inventoryOpened = false;
    public bool disableInventory = false;


    private void Awake()
    {
        EventInput.disableDash.AddListener(DisableDash);
        EventInput.disableDefence.AddListener(DisableDefence);
        EventInput.disableMovement.AddListener(DisableMovement);
        EventInput.disableMouse.AddListener(DisableMouse);
        EventInput.disableInventory.AddListener(DisableInventory);
    }


    private void Update()
    {
        WASDMovement();
        Dash();
        OpenCloseInventory();
    }


    public Vector3 WASDMovement()
    {
        if (!disableMovement)
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            Vector3 movement = GameManager.instance.playerBehaviour.transform.right * moveX +
                               GameManager.instance.playerBehaviour.transform.forward * moveZ;
            movement.y = Physics.gravity.y * Time.deltaTime;
            movement = movement.normalized * Time.deltaTime;

            if (moveX != 0 || moveZ != 0)
                inputWASD = true;
            else
                inputWASD = false;

            return movement;
        }

        return new Vector3(0, 0, 0);
    }

    void DisableMovement(bool _value)
    {
        disableMovement = _value;
    }


    // Dashing
    public bool Dash()
    {
        if (!disableDash)
        {
            if (Input.GetKey(KeyCode.Space))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    void DisableDash(bool _value)
    {
        disableDash = _value;
    }


    // MouseActions
    public bool LeftMouseButton()
    {
        if (!disableMouse)
            if (Input.GetKeyDown(KeyCode.Mouse0))
                return true;
            else
                return false;
        return false;
    }

    public bool RightMouseButton()
    {
        if (!disableMouse && !disableDefence)
            if (Input.GetKey(KeyCode.Mouse1))
                return true;
            else
                return false;
        return false;
    }

    void DisableMouse()
    {
        disableMouse = !disableMouse;
    }

    void DisableDefence(bool _value)
    {
        disableDefence = _value;
    }


    // Inventory
    void OpenCloseInventory()
    {
        if (!disableInventory)
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventoryOpened = !inventoryOpened;
                DisableMouse();
                EventInput.inventoryOpen.Invoke();
            }
    }

    void DisableInventory()
    {
        disableInventory = !disableInventory;
    }

    // Interaction
    public bool Interact()
    {
        if (Input.GetKey(KeyCode.F))
            return true;
        else
            return false;
    }
}