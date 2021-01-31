using Systems.Player;
using Systems.Stats;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Ui.Gui
{
    public class GameUiBehaviour : MonoBehaviour
    {
        [SerializeField] public Slider healthBar;
        [SerializeField] public Slider energyBar;
        [SerializeField] public Slider experienceBar;
        [SerializeField] public GameObject inventoryPanel;

        private bool _inventoryEnabled = false;
        private PlayerBehaviour _playerBehaviour;
        
        
        
        private void Awake()
        {
            EventInput.inventoryOpen.AddListener(InventorySetActive);
            EventStats.UpdateStats.AddListener(UpdateStats);
        }
        
        private void Start ()
        {
            _playerBehaviour = GameManager.instance.playerBehaviour;
            inventoryPanel.SetActive(_inventoryEnabled);
            EventStats.UpdateStats.Invoke();
        }
        
        private void InventorySetActive()
        {
            _inventoryEnabled = !_inventoryEnabled;
            inventoryPanel.SetActive(_inventoryEnabled);
        }

        private void UpdateStats()
        {
            healthBar.value = _playerBehaviour.Health.Stat;
            energyBar.value = _playerBehaviour.Energy.Stat;
        }
    }
}
