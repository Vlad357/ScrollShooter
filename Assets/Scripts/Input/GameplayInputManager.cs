using ScrollShooter.Player;
using System;
using UnityEngine;

namespace ScrollShooter.Input
{
    [RequireComponent(typeof(PlayerMovementInput))]
    public class GameplayInputManager : MonoBehaviour
    {
        public event Action<float> MovementPlayerAxisReceived;
        public event Action JumpEventRecived;
        public event Action SwitchWeapon;
        public event Action Attack;

        public event Action CansledActiveAction;

        public RectTransform TouchedPanel;
        
        private PlayerMovementInput _playerMovementInput;
        private PlayerWeaponInput _playerWeaponInput;
        private PlayerInputMap _input;

        private void Start()
        {
            _playerMovementInput = GetComponent<PlayerMovementInput>();
            _input = new PlayerInputMap();
            _playerWeaponInput = new PlayerWeaponInput(_input);

            _input.Enable();

            InputsInit(_input);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        public void CanselActivAction()
        {
            CansledActiveAction?.Invoke();
        }

        private void InputsInit(PlayerInputMap input)
        {
            _playerMovementInput.SetPlayerInput(input);
            _playerMovementInput.MovementAxisReceived += OnMovementAxisReceived;
            _playerMovementInput.JumpEvent += PlayerMovementInputOnJumpEvent;
            
            _playerWeaponInput.OnSwitchWeapon += PlayerWeaponInputOnOnSwitchWeapon;
            _playerWeaponInput.OnAttackWeapon += PlayerWeaponInputOnAttack;
        }

        private void PlayerMovementInputOnJumpEvent()
        {
            CanselActivAction();
            JumpEventRecived?.Invoke();
        }

        private void OnMovementAxisReceived(float axisValue)
        {
            MovementPlayerAxisReceived?.Invoke(axisValue);
        }

        private void PlayerWeaponInputOnOnSwitchWeapon()
        {
            SwitchWeapon?.Invoke();
        }
        
        private void PlayerWeaponInputOnAttack()
        {
            Vector2 touchPosition = _input.Player.TouchPosition.ReadValue<Vector2>();
            bool touch = RectTransformUtility.RectangleContainsScreenPoint(TouchedPanel, touchPosition);

            if (touch && TouchedPanel.gameObject.activeSelf)
            {
                Attack?.Invoke();
            }
        }
    }
}