using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScrollShooter
{
    public class PlayerWeaponInput
    {
        public event Action OnSwitchWeapon;
        
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
            input.Player.SwitchWeapon.started += SwitchWeaponOnperformed;
        }

        private void SwitchWeaponOnperformed(InputAction.CallbackContext context)
        {
            Debug.Log("switch");
            Debug.Log(context.ReadValue<float>());
            OnSwitchWeapon?.Invoke();
        }
    }
}