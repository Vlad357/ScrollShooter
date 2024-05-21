using ScrollShooter.EntityScripts;
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
        protected override void Init()
        {
            base.Init();
            _gameplayInput = GetComponent<GameplayInputManager>();

            InitGameplayInput(_gameplayInput);
        }


        private void InitGameplayInput(GameplayInputManager gameplayInputManager)
        {
            gameplayInputManager.MovementPlayerAxisReceived += OnMovementEntityAxisReceived;
            gameplayInputManager.JumpEventRecived += OnJumpEventRecived;
        }

    }
}