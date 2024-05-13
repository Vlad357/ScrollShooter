using ScrollShooter.Entity;
using ScrollShooter.Input;
using UnityEngine;

namespace ScrollShooter.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GameplayInputManager))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : EntityMovement
    {

        protected GameplayInputManager _gameplayInput;
        private void Start()
        {
            _gameplayInput = GetComponent<GameplayInputManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            InitGameplayInput(_gameplayInput);
        }


        private void InitGameplayInput(GameplayInputManager gameplayInputManager)
        {
            gameplayInputManager.MovementPlayerAxisReceived += OnMovementPlayerAxisReceived;
            gameplayInputManager.JumpEventRecived += OnJumpEventRecived;
        }

    }
}