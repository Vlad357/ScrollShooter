using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScrollShooter.Player
{
    public class PlayerWeaponInput
    {
        public event Action OnSwitchWeapon;
        public event Action OnAttackWeapon;
        
        private PlayerInputMap _input;

        public PlayerWeaponInput(PlayerInputMap input)
        {
            _input = input;

            InitInputPort(_input);
        }
        
        private void InitInputPort(PlayerInputMap input)
        {
            input.Player.SwitchWeapon.started  += SwitchWeaponOnperformed;
            input.Player.Attack.started += AttackOnStarted;
        }

        private void AttackOnStarted(InputAction.CallbackContext obj)
        {
            OnAttackWeapon?.Invoke();
        }

        private void SwitchWeaponOnperformed(InputAction.CallbackContext context)
        {
            OnSwitchWeapon?.Invoke();
        }
    }
}