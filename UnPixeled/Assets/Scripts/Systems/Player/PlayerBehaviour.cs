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

        [Header("Inventory and equipment")]
        [SerializeField] private Inventory.Inventory inventory;
        public Inventory.Inventory Inventory
        {
            get => inventory;
            set => inventory = value;
        }

        [SerializeField] private Equipment equipment;
        public Equipment Equipment
        {
            get => equipment;
            set => equipment = value;
        }
        
        [Header("Prefab GameObjects")] 
        [SerializeField] private GameObject playerModel;
        [SerializeField] private GameObject weaponHandler;
        public GameObject WeaponHandler
        {
            get => weaponHandler;
            set => weaponHandler = value;
        }

        private Animator _animator;
        private CharacterController _charController;
        
        private PlayerAnimator _playerAnimator;
        public PlayerAnimator PlayerAnimator => _playerAnimator;
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
            EventInventory.useItem.AddListener(equipment.UseItem);
            EventInventory.updateInventory.Invoke();
            EventInventory.clearInventory.AddListener(inventory.Clear);
            EventInventory.clearInventory.AddListener(equipment.Clear);
            //EventStats.ChangePlayerHealth.AddListener(health.ChangeStat);
            //EventStats.ChangePlayerEnergy.AddListener(energy.ChangeStat);
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