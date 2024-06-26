using ScrollShooter.Supports;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    public class EntityAttack : MonoBehaviour
    {
        public event Action<bool> OnAttackProcess;

        public event Action<EntityStats> OnSetCurrentAmmo;

        public GameObject projecctileObject;

        public AudioClip shootSound;
        public AudioClip hitSound;

        [SerializeField] protected LayerMask _enemyLayerMask;
        [SerializeField] protected Vector2 _spawnProjectileOffset;

        protected AudioSource _audioSource;

        protected Animator _animator;
        protected Entity _entity;

        protected bool _attackReady = true;
        protected bool _attackIsInpossible = false;

        private void Start()
        {
            Init();
        }

        public void MelleAttack()
        {
            float attackRadius = _entity.EntityCurrentStats.attackRadius;

            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(transform.position, attackRadius, _enemyLayerMask);

            List<GameObject> enemiesDamaged = new List<GameObject>();

            foreach (var enemy in enemies)
            {
                if (!enemy.isTrigger && !enemiesDamaged.Contains(enemy.gameObject))
                {
                    EntityHealthHandler entityHealthHandler = enemy.GetComponent<EntityHealthHandler>();
                    SetDamageOnEntityHealthHandler(entityHealthHandler);
                    enemiesDamaged.Add(enemy.gameObject);
                }
            }
        }

        public void SpawnProjectile()
        {
            if (_entity.EntityCurrentStats.currentAmmo > 0)
            {
                EntityStats entityStats = new EntityStats();
                entityStats.currentAmmo = -1;

                OnSetCurrentAmmo?.Invoke(entityStats);

                OnSpawnRangeProjectile();
            }
        }

        public void ReadyAttackTurnOn()
        {
            _attackReady = true;
        }

        public void ReadyAttackTurnOff()
        {
            _attackReady = false;
        }

        public void AttackProcessTurnOn()
        {
            _animator.SetBool(EntityAnimatorParameters.ATTACK_PROCESS, true);
            OnAttackProcess?.Invoke(true);
        }

        public void AttackProcessTurnOff()
        {
            _animator.SetBool(EntityAnimatorParameters.ATTACK_PROCESS, false);
            OnAttackProcess?.Invoke(false);
        }

        public void PlayShotSound()
        {
            _audioSource.clip = shootSound;
            _audioSource.Play();
        }

        public void PlayHitSound()
        {
            _audioSource.clip = hitSound;
            _audioSource.Play();
        }

        protected virtual void Init()
        {
            _animator = GetComponent<Animator>();
            _entity = GetComponent<Entity>();

            _audioSource = GetComponentInChildren<AudioSource>();

            _entity.OnStartJumpEvent += AttackProcessTurnOff;
            _entity.OnDeath += AttackIsInpossible;
        }

        protected void SetDamageOnEntityHealthHandler(EntityHealthHandler entityHealthHandler)
        {
            float attackDamage = _entity.EntityCurrentStats.damageMelleAttack;

            entityHealthHandler.SetDamage(attackDamage, gameObject);
        }

        protected virtual void OnAttack()
        {
            if (_attackReady && !_attackIsInpossible)
            {
                _animator.SetTrigger(EntityAnimatorParameters.ATTACK);
            }
        }

        private void AttackIsInpossible()
        {
            _attackIsInpossible = true;
        }

        private void OnSpawnRangeProjectile()
        {
            Vector2 spawnProjectilePosition = new Vector2
                (transform.position.x + _spawnProjectileOffset.x * transform.localScale.x,
                transform.position.y + _spawnProjectileOffset.y);

            Instantiate(projecctileObject, spawnProjectilePosition, Quaternion.identity)
                .GetComponent<RangeProjectile>().ParametersInit(transform.localScale.x, gameObject);
        }
    }
}