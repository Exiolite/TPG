//Copyright Ex/IO 2020
using System.Collections.Generic;
using UnityEngine.PlayerLoop;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] public Slider healthBar;
    [SerializeField] public Slider energyBar;
    [SerializeField] public Slider expirienceBar;
    [SerializeField] public GameObject InventoryPannell;
    bool inventoryEnabled = false;



    private void Awake()
    {
        EventManager.inventoryOpen.AddListener(InventorySetActive);
        EventManager.updatePlayerStats.AddListener(UpdateBars);
    }

    void Start ()
    {
        InventoryPannell.SetActive(inventoryEnabled);
    }



    void InventorySetActive ()
    {
        inventoryEnabled = !inventoryEnabled;
        InventoryPannell.SetActive(inventoryEnabled);
        if (GameManager.instance.playerBehaviour.weaponCollider)
        {
            GameManager.instance.playerBehaviour.weaponCollider.enabled = false;
        }
    }


    void UpdateBars (float _health, float _energy, float _expirience)
    {
        healthBar.value = _health;
        energyBar.value = _energy;
        expirienceBar.value = _expirience;
    }
}
