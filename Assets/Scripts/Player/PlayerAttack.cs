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
        public GameObject projecctileObject;
        
        private GameplayInputManager _gameplayInputManager;

        private void OnSpawnPlayerRangeProjectile()
        {
            Vector2 spawnProjectilePosition =
                    new Vector2(transform.position.x + transform.localScale.x/2, transform.position.y);

            Instantiate(projecctileObject, spawnProjectilePosition, Quaternion.identity)
                .GetComponent<RangeProjectile>().ParametersInit(transform.localScale.x, gameObject);
        }

        protected override void Init()
        {
            base.Init();

            _gameplayInputManager = GetComponent<GameplayInputManager>();

            _gameplayInputManager.Attack += OnAttack;
            _gameplayInputManager.CansledActiveAction += ReadyAttackTurnOn;

            OnSpawnRangeProjectile += OnSpawnPlayerRangeProjectile;
        }
    }
}