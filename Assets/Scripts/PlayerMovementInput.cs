using System;
using UnityEngine;

namespace ScrollShooter
{
    public class PlayerMovementInput : MonoBehaviour
    {
        public event Action<float> MovementAxisReceived;

        private PlayerInputMap _input;

        public void SetPlayerInput(PlayerInputMap input)
        {
            this._input = input;
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