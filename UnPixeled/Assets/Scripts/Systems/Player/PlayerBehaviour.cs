using Systems.Inventory;
using Systems.Stats;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private float dashSpeed;
        
        [Header("Stat parameters")]
        [SerializeField] private StatParameter health;
        public StatParameter Health => health;
        [SerializeField] private StatParameter energy;
        public StatParameter Energy => energy;

        [Header("Inventory")]
        [SerializeField] private Inventory.Inventory inventory;
        public Inventory.Inventory Inventory
        {
            get => inventory;
            set => inventory = value;
        }
        
        [Header("Prefab GameObjects")] 
        [SerializeField] private GameObject playerModel;
        [SerializeField] private GameObject weaponHandler;

        private Animator _animator;
        private CharacterController _charController;
        
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _playerMovement;
        
        
        
        private void Awake()
        {
            InitializeComponents();
            InitializeModules();
        }

        private void InitializeComponents()
        {
            _charController = GetComponent<CharacterController>();
            _animator = playerModel.GetComponent<Animator>();
        }

        private void InitializeModules()
        {
            _playerMovement = new PlayerMovement(_charController, playerModel, speed, dashSpeed);
            _playerAnimator = new PlayerAnimator(_animator);
        }
        
        private void Start()
        {
            EventInventory.addItem.AddListener(inventory.AddItem);
            EventInventory.removeItem.AddListener(inventory.RemoveItem);
            EventInventory.useItem.AddListener(inventory.UseItem);
            EventInventory.updateInventory.Invoke();
            EventStats.changePlayerHealth.AddListener(health.ChangeStat);
            EventStats.changePlayerEnergy.AddListener(energy.ChangeStat);
        }
        
        private void Update()
        {
            UpdateModules();
        }

        private void UpdateModules()
        {
            _playerMovement.UpdatePlayerMovement();
            _playerAnimator.UpdateAnimation();
        }
    }
}