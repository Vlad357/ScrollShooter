using ScrollShooter.EntityScripts;
using ScrollShooter.Input;
using ScrollShooter.Supports;
using UnityEngine;

namespace ScrollShooter.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameplayInputManager))]
    public class PlayerAttack : EntityAttack
    {
        private GameplayInputManager _gameplayInputManager;

        protected override void Init()
        {
            base.Init();

            _gameplayInputManager = GetComponent<GameplayInputManager>();

            _gameplayInputManager.Attack += OnAttack;
            _gameplayInputManager.CansledActiveAction += ReadyAttackTurnOn;
        }
    }
}