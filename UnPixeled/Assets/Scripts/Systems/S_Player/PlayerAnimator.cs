using UnityEngine;

namespace Systems.S_Player
{
    public class PlayerAnimator
    {
        private Animator _playerAnim;

        public void InitializeComponent(Animator playerAnim)
        {
            _playerAnim = playerAnim;
        }

        public void UpdateAnimation()
        {
            MovementAnimations(GameManager.instance.inputManager.inputWASD);
            UpdateWeaponAnimation();
        }
        
        private void MovementAnimations(bool flag)
        {
            if (flag)
            {
                _playerAnim.SetInteger("move", 1);
            }
            else
            {
                _playerAnim.SetInteger("move", 0);
            }
        }
        
        private void UpdateWeaponAnimation()
        {
            AttackAnimation();
            DefenceAnimation();
        }


        void AttackAnimation()
        {

        }

        void DefenceAnimation()
        {

        }
    }
}