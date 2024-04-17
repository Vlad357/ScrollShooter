using System;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameplayInputManager))]
    public class PlayerWeaponSwitcher : MonoBehaviour
    {
        private GameplayInputManager _gameplayInputManager;
        private Animator _animator;

        private void Start()
        {
            _gameplayInputManager = GetComponent<GameplayInputManager>();
            _animator = GetComponent<Animator>();

            GameplayManagerInit(_gameplayInputManager);
        }

        private void GameplayManagerInit(GameplayInputManager gameplay)
        {
            gameplay.SwitchWeapon += OnSwitchWeapon;
        }

        private void OnSwitchWeapon()
        {
            _animator.SetTrigger(PlayerAnimatorParameters.SWAP_ARMED);
            int weaponNumber = _animator.GetInteger(PlayerAnimatorParameters.WEAPON_NUMBER);
            weaponNumber = weaponNumber == 0 ? 1 : 0;
            _animator.SetInteger(PlayerAnimatorParameters.WEAPON_NUMBER, weaponNumber);
        }
    }
}