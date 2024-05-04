using System;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameplayInputManager))]
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject projecctileObject;
        public TextMeshProUGUI ammoTextMeshCounter;
        
        [SerializeField] private LayerMask enemyLayerMask;

        [SerializeField] private float melleAttackRadius;
        [SerializeField] private float damageMelleAttack = 10f;
        
        [SerializeField] private int maxAmmo = 10;
        [SerializeField] private int currentAmmo;

        public int CurrentAmmo
        {
            get
            {
                return currentAmmo;
            }
            private set
            {
                currentAmmo = value;
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
        
        private GameplayInputManager _gameplayInputManager;
        private Animator _animator;

        private bool _attackReady = true;
        
        private void Start()
        {
            _gameplayInputManager = GetComponent<GameplayInputManager>();
            _animator = GetComponent<Animator>();
            
            _gameplayInputManager.Attack += GameplayInputManagerOnAttack;
            _gameplayInputManager.CansledActiveAction += ReadyAttackTurnOn;

            ReloadAmmo();
        }
        
        public void MelleAttack()
        {
            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(transform.position, melleAttackRadius, enemyLayerMask);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EntityHealth>().SetDamage(damageMelleAttack, gameObject);
                Debug.Log(enemy.name);
            }
        }

        public void SpawnProjectile()
        {
            if (CurrentAmmo > 0)
            {
                CurrentAmmo -= 1;
            
                Vector2 spawnProjectilePosition =
                    new Vector2(transform.position.x + transform.localScale.x, transform.position.y);
                Instantiate(projecctileObject, spawnProjectilePosition, Quaternion.identity)
                    .GetComponent<RangeProjectile>().ParametersInit(transform.localScale.x, gameObject);
                Debug.Log("range projectile spawned");
            }
        }
        
        public void ReadyAttackTurnOn()
        {
            _attackReady = true;
            _animator.SetBool(PlayerAnimatorParameters.ATTACK_PROCESS, false);
        }
        
        public void ReadyAttackTurnOff()
        {
            _attackReady = false;
            _animator.SetBool(PlayerAnimatorParameters.ATTACK_PROCESS, true);
        }
        
        private void GameplayInputManagerOnAttack()
        {
            if (_attackReady)
            {
                _animator.SetTrigger(PlayerAnimatorParameters.ATTACK);
            }
        }
        
        private void ReloadAmmo()
        {
            CurrentAmmo = maxAmmo;
        }
    }
}