using Core.Managers.Game;
using UnityEngine;

namespace Systems.S_Player
{
    public class PlayerMovement
    {
        private float playerSpeed = 20;
        private float dashSpeed = 100;

        private float _currentPlayerSpeed;

        private CharacterController _characterController;
        private GameObject _playerModel;


        public void InitializeComponent(CharacterController characterController, GameObject playerModel)
        {
            _characterController = characterController;
            _playerModel = playerModel;
            _currentPlayerSpeed = playerSpeed;
        }

        public void UpdatePlayerMovement()
        {
            SetPlayerRotation();
            RotatePlayer();
            MovePlayer();
        }


        private void SetPlayerRotation()
        {
            if (GameManager.instance.inputManager.inputWASD)
            {
                var angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
                _playerModel.transform.rotation = Quaternion.Euler(new Vector3(0, angle + 45, 0));
            }
        }

        private void RotatePlayer()
        {
            if (GameManager.instance.inputManager.Dash())
            {
                _currentPlayerSpeed = dashSpeed;
            }
            else
            {
                _currentPlayerSpeed = playerSpeed;
            }
        }

        private void MovePlayer()
        {
            _characterController.Move(GameManager.instance.inputManager.WASDMovement() * _currentPlayerSpeed);
        }
    }
}