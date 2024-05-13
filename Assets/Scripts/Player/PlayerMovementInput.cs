using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScrollShooter.Player
{
    public class PlayerMovementInput : MonoBehaviour
    {
        public event Action<float> MovementAxisReceived;
        public event Action JumpEvent;

        private PlayerInputMap _input;

        public void SetPlayerInput(PlayerInputMap input)
        {
            this._input = input;
            
            _input.Player.Jump.performed += JumpOnperformed;
        }

        private void JumpOnperformed(InputAction.CallbackContext obj)
        {
            JumpEvent?.Invoke();
        }

        private void Movement()
        {
            float axis = _input.Player.Movement.ReadValue<float>();
            MovementAxisReceived?.Invoke(axis);
        }

        private void FixedUpdate()
        {
           Movement();
        }
    }
}