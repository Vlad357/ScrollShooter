using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    public class EntityAttack : MonoBehaviour
    {
        public event Action OnSpawnRangeProjectile;
        public event Action<EntityStats> OnSetCurrentAmmo;

        [SerializeField] protected LayerMask enemyLayerMask;

        protected Animator _animator;
        protected Entity _entity;

        protected bool _attackReady = true;

        private void Start()
        {
            Init();
        }

        public void MelleAttack()
        {
            float attackRadius = _entity.EntityCurrentStats.melleAttackRadius;
            float attackDamage = _entity.EntityCurrentStats.damageMelleAttack;

            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayerMask);

            foreach (var enemy in enemies)
            {
                if (!enemy.isTrigger)
                {
                    enemy.GetComponent<EntityHealthHandler>().SetDamage(attackDamage, gameObject);
                    Debug.Log(enemy.name);
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

                OnSpawnRangeProjectile?.Invoke();
            }
        }

        public void ReadyAttackTurnOn()
        {
            _attackReady = true;
            _animator.SetBool(EntityAnimatorParameters.ATTACK_PROCESS, false);
        }

        public void ReadyAttackTurnOff()
        {
            _attackReady = false;
            _animator.SetBool(EntityAnimatorParameters.ATTACK_PROCESS, true);
        }

        protected virtual void Init()
        {
            _animator = GetComponent<Animator>();
            _entity = GetComponent<Entity>();
        }

        protected void OnAttack()
        {
            print("attack");

            if (_attackReady)
            {

                _animator.SetTrigger(EntityAnimatorParameters.ATTACK);
            }
        }
    }
}