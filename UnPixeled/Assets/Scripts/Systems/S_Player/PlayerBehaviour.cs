using UnityEngine;
using Systems.S_Inventory;

namespace Systems.S_Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _playerMovement;

        [SerializeField] private InventoryContainer inventoryContainer;
        private Inventory _inventory;
        public Inventory Inventory => _inventory;

        private Animator _animator;
        private CharacterController _charController;

        [Header("Components")] [SerializeField]
        private GameObject playerModel;

        [SerializeField] private GameObject weaponHandler;
        public GameObject WeaponHandler => weaponHandler;
        [SerializeField] private Inventory playerInventory;
        public Inventory PlayerInventory => playerInventory;

        public PlayerBehaviour(Inventory inventory)
        {
            _inventory = inventory;
        }


        private void Awake()
        {
            InitializeComponents();
            InitializeModules();
        }

        private void Start()
        {
            EventInventory.addItem.AddListener(_inventory.AddItem);
            EventInventory.removeItem.AddListener(_inventory.RemoveItem);
            EventInventory.useItem.AddListener(_inventory.UseItem);
        }

        private void Update()
        {
            UpdateModules();
        }

        private void InitializeComponents()
        {
            _charController = GetComponent<CharacterController>();
            _animator = playerModel.GetComponent<Animator>();
        }

        private void InitializeModules()
        {
            _playerMovement = new PlayerMovement();
            _playerMovement.InitializeComponent(_charController, playerModel);

            _playerAnimator = new PlayerAnimator();
            _playerAnimator.InitializeComponent(_animator);

            _inventory = new Inventory();
            _inventory.SetContainer(inventoryContainer);
        }

        private void UpdateModules()
        {
            _playerMovement.UpdatePlayerMovement();
            _playerAnimator.UpdateAnimation();
        }
    }
}