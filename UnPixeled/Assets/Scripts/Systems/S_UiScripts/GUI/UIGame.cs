//Copyright Ex/IO 2020

using Systems.S_HealthStats;
using Core.Managers.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.S_UiScripts.GUI
{
    public class UiGame : MonoBehaviour
    {
        [SerializeField] public Slider healthBar;
        [SerializeField] public Slider energyBar;
        [SerializeField] public Slider experienceBar;
        [SerializeField] public GameObject inventoryPannell;
        private bool _inventoryEnabled = false;



        private void Awake()
        {
            EventInput.inventoryOpen.AddListener(InventorySetActive);
        
            EventStats.updatePlayerHealth.AddListener(UpdatePlayerHealth);
            EventStats.updatePlayerEnergy.AddListener(UpdatePlayerEnergy);
            EventStats.updatePlayerExperience.AddListener(UpdatePlayerExpirience);
        }

    
    
        void Start ()
        {
            inventoryPannell.SetActive(_inventoryEnabled);
        }
    
    
    
        private void InventorySetActive()
        {
            _inventoryEnabled = !_inventoryEnabled;
            inventoryPannell.SetActive(_inventoryEnabled);
        }

        private void UpdatePlayerHealth(float value)
        {
            healthBar.value = value;
        }
        private void UpdatePlayerEnergy(float value)
        {
            energyBar.value = value;
        }
        private void UpdatePlayerExpirience(float value)
        {
            experienceBar.value = value;
        }
    
    }
}
