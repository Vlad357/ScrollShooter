using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScrollShooter
{
    public class PlayerWeaponInput
    {
        public event Action OnSwitchWeapon;
        public event Action OnAttackWeapon;
        
        private PlayerInputMap _input;

        private float _mouseScrollY;

        public PlayerWeaponInput(PlayerInputMap input)
        {
            _input = input;

            InitInputPort(_input);
        }
        
        private void InitInputPort(PlayerInputMap input)
        {
            Debug.Log("init");
            input.Player.SwitchWeapon.started  += SwitchWeaponOnperformed;
            input.Player.Attack.performed += AttackOnperformed;
        }

        private void AttackOnperformed(InputAction.CallbackContext obj)
        {
            OnAttackWeapon?.Invoke();
        }

        private void SwitchWeaponOnperformed(InputAction.CallbackContext context)
        {
            OnSwitchWeapon?.Invoke();
        }
    }
}