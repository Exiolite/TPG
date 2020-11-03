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
        EventGame.inventoryOpen.AddListener(InventorySetActive);
        EventGame.updatePlayerStats.AddListener(UpdateBars);
    }

    void Start ()
    {
        InventoryPannell.SetActive(inventoryEnabled);
    }



    void InventorySetActive ()
    {
        inventoryEnabled = !inventoryEnabled;
        InventoryPannell.SetActive(inventoryEnabled);
        if (GameManager.instance.playerManager.weaponCollider)
        {
            GameManager.instance.playerManager.weaponCollider.enabled = false;
        }
    }


    void UpdateBars (float _health, float _energy, float _expirience)
    {
        healthBar.value = _health;
        energyBar.value = _energy;
        expirienceBar.value = _expirience;
    }
}
