using Core;
using UnityEngine;

namespace Systems.Player
{
    public class PlayerAnimator
    {
        private readonly Animator _playerAnim;
        private bool _isAttacking;
        public bool IsAttacking => _isAttacking;

        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int StuffAttack = Animator.StringToHash("StuffAttack");
        private static readonly int Attack1 = Animator.StringToHash("Attack");

        
        
        public PlayerAnimator(Animator playerAnim)
        {
            _playerAnim = playerAnim;
        }
        
        public void UpdateAnimation()
        {
            MovementAnimations(GameManager.instance.inputManager.inputWasd);
            UpdateWeaponAnimation();
        }
        
        
        
        private void MovementAnimations(bool flag)
        {
            _playerAnim.SetInteger(Move, flag ? 1 : 0);
        }
        
        private void UpdateWeaponAnimation()
        {
            Attack();
            Defence();
        }

        private void Attack()
        {
            if (GameManager.instance.inputManager.LeftMouseButton())
            {
                _playerAnim.SetInteger(StuffAttack, 1);
                _isAttacking = true;
            }
            else if (Input.GetAxis("Attack") == 0)
            {
                _playerAnim.SetInteger(StuffAttack, 0);
                _playerAnim.SetInteger(Attack1, 0);
                _isAttacking = false;
            }
            else
            {
                _playerAnim.SetInteger(StuffAttack, 0);
                _playerAnim.SetInteger(Attack1, 0);
            }
        }

        private void Defence()
        {

        }
    }
}