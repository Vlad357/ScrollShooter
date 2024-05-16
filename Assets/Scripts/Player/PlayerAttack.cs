using ScrollShooter.Entity;
using ScrollShooter.Input;
using ScrollShooter.Supports;
using TMPro;
using UnityEngine;

namespace ScrollShooter.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameplayInputManager))]
    public class PlayerAttack : EntityAttack
    {
        public TextMeshProUGUI ammoTextMeshCounter;
        public GameObject projecctileObject;
        
        private GameplayInputManager _gameplayInputManager;

        public override int CurrentAmmo
        {
            get
            {
                return _currentAmmo;
            }

            set
            {
                _currentAmmo = value;
                try
                {
                    ammoTextMeshCounter.text = CurrentAmmo.ToString();
                }
                catch
                {
                    Debug.LogError($"ammoTextMeshCounter has not assigned");
                }
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _gameplayInputManager = GetComponent<GameplayInputManager>();
            
            _gameplayInputManager.Attack += OnAttack;
            _gameplayInputManager.CansledActiveAction += ReadyAttackTurnOn;

            OnSpawnRangeProjectile += OnSpawnPlayerRangeProjectile;

            ReloadAmmo();
        }

        private void OnSpawnPlayerRangeProjectile()
        {
            Vector2 spawnProjectilePosition =
                    new Vector2(transform.position.x + transform.localScale.x, transform.position.y);

            Instantiate(projecctileObject, spawnProjectilePosition, Quaternion.identity)
                .GetComponent<RangeProjectile>().ParametersInit(transform.localScale.x, gameObject);
        }
    }
}