using System;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(PlayerMovementInput))]
    public class GameplayInputManager : MonoBehaviour
    {
        public event Action<float> MovementPlayerAxisReceived;
        
        private PlayerMovementInput _playerMovementInput;
        private PlayerInputMap _input;

        private void Start()
        {
            _playerMovementInput = GetComponent<PlayerMovementInput>();
            _input = new PlayerInputMap();
            
            _input.Enable();

            InputsInit(_input);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void InputsInit(PlayerInputMap input)
        {
            _playerMovementInput.SetPlayerInput(input);
            _playerMovementInput.MovementAxisReceived += OnMovementAxisReceived;
        }

        private void OnMovementAxisReceived(float axisValue)
        {
            MovementPlayerAxisReceived?.Invoke(axisValue);
        }
    }
}