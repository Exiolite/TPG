using Core;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerMovement
    {
        private readonly CharacterController _characterController;
        private readonly GameObject _playerModel;
        
        private readonly float _playerSpeed;
        private readonly float _dashSpeed;
        private float _currentPlayerSpeed;


        
        
        public PlayerMovement(CharacterController characterController, GameObject playerModel, float playerSpeed, float dashSpeed)
        {
            _characterController = characterController;
            _playerModel = playerModel;
            _playerSpeed = playerSpeed;
            _dashSpeed = dashSpeed;
        }

        public void UpdatePlayerMovement()
        {
            SetPlayerRotation();
            RotatePlayer();
            MovePlayer();
        }


        
        
        private void SetPlayerRotation()
        {
            if (!GameManager.instance.inputManager.inputWasd) return;
            var angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
            _playerModel.transform.rotation = Quaternion.Euler(new Vector3(0, angle + 45, 0));
        }

        private void RotatePlayer()
        {
            _currentPlayerSpeed = GameManager.instance.inputManager.Dash() ? _dashSpeed : _playerSpeed;
        }

        private void MovePlayer()
        {
            _characterController.Move(GameManager.instance.inputManager.GetMovementVector() * _currentPlayerSpeed);
        }
    }
}