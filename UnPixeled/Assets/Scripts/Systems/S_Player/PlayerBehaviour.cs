//Copyright Ex/IO 2020

using System;
using Systems.S_HealthStats;
using Systems.S_Inventory;
using Core.Managers;
using UnityEngine;

namespace Systems.S_Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _playerMovement;
        private PlayerInventory _playerInventory;
        private PlayerInteraction _playerInteraction;
        
        private Animator _animator;
        private CharacterController _charController;
        
        [Header("Components")]
        [SerializeField] private GameObject playerModel;
        public GameObject WeaponHandler => weaponHandler;
        [SerializeField] private GameObject weaponHandler;
        public Inventory PlayerInventory => playerInventory;
        [SerializeField] private Inventory playerInventory;
        public Equipment PlayerEquipment => playerEquipment;
        [SerializeField] private Equipment playerEquipment;
        
        
        
        private void Awake()
        {
            InitializeComponents();
            InitializeModules();
        }

        private void Start()
        {
            EventManager.playerDead.AddListener(_playerInventory.ResetPlayer);
            EventInventory.inventoryAction.AddListener(_playerInventory.PlayerInventoryAction);
            EventInventory.equipmentAction.AddListener(_playerInventory.PlayerEqupmentAction);
        }

        private void OnTriggerStay(Collider other)
        {
            _playerInteraction.Interact(other);
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
            
            _playerInventory = new PlayerInventory();
            _playerInventory.InitializeComponent(playerInventory, playerEquipment);
            
            _playerInteraction = new PlayerInteraction();
        }

        private void UpdateModules()
        {
            _playerMovement.UpdatePlayerMovement();
            _playerAnimator.UpdateAnimation();
        }

        private void OnApplicationQuit()
        {
            playerInventory.container.Clear();
            playerEquipment.Clear();
        }
    }
}