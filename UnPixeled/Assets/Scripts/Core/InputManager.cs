using UnityEngine;

namespace Core
{
    public class InputManager : MonoBehaviour
    {
        private Transform _playerTransform;
        
        private float _moveX, _moveZ;
        public bool inputWasd;
        public bool disableMovement;
        public bool disableDash;
        public bool disableMouse = false;
        public bool disableDefence = false;
        public bool inventoryOpened = false;
        public bool disableInventory = false;


        private void Awake()
        {
            _playerTransform = GameManager.instance.playerBehaviour.transform;
            EventInput.disableDash.AddListener(DisableDash);
            EventInput.disableDefence.AddListener(DisableDefence);
            EventInput.disableMovement.AddListener(DisableMovement);
            EventInput.disableMouse.AddListener(DisableMouse);
            EventInput.disableInventory.AddListener(DisableInventory);
        }

        private void Update()
        {
            GetMovementVector();
            Dash();
            OpenCloseInventory();
            Interact();
        }
        
        public Vector3 GetMovementVector()
        {
            if (disableMovement) return new Vector3(0, 0, 0);
            
            _moveX = Input.GetAxis("Horizontal");
            _moveZ = Input.GetAxis("Vertical");
            var movement = _playerTransform.right * _moveX + _playerTransform.forward * _moveZ;
            movement.y = Physics.gravity.y * Time.deltaTime;
            movement = movement.normalized * Time.deltaTime;

            if (_moveX != 0 || _moveZ != 0)
                inputWasd = true;
            else
                inputWasd = false;

            return movement;

        }

        private void DisableMovement(bool value)
        {
            disableMovement = value;
        }
        
        public bool Dash()
        {
            return !disableDash && Input.GetKey(KeyCode.Space);
        }

        private void DisableDash(bool value)
        {
            disableDash = value;
        }

        public bool LeftMouseButton()
        {
            return !disableMouse && Input.GetKeyDown(KeyCode.Mouse0);
        }

        public bool RightMouseButton()
        {
            return !disableMouse && !disableDefence && Input.GetKey(KeyCode.Mouse1);
        }

        private void DisableMouse()
        {
            disableMouse = !disableMouse;
        }

        private void DisableDefence(bool value)
        {
            disableDefence = value;
        }
        
        private void OpenCloseInventory()
        {
            if (disableInventory) return;
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            inventoryOpened = !inventoryOpened;
            DisableMouse();
            EventInput.inventoryOpen.Invoke();
        }

        private void DisableInventory()
        {
            disableInventory = !disableInventory;
        }

        private static void Interact()
        {
            if (Input.GetKeyDown(KeyCode.F)) EventInput.interact.Invoke();
        }
    }
}