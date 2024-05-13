using System;
using UnityEngine;

namespace ScrollShooter.Entity
{
    public class EntityAttack : MonoBehaviour
    {
        public event Action OnSpawnRangeProjectile;

        [SerializeField] protected LayerMask enemyLayerMask;

        protected Animator _animator;

        protected float _melleAttackRadius = 2f;
        protected float _damageMelleAttack = 10f;

        protected int _maxAmmo = 10;
        protected int _currentAmmo;

        protected bool _attackReady = true;

        public virtual int CurrentAmmo
        {
            get
            {
                return _currentAmmo;
            }
            protected set
            {
                _currentAmmo = value;
            }
        }

        public void MelleAttack()
        {
            Collider2D[] enemies =
                Physics2D.OverlapCircleAll(transform.position, _melleAttackRadius, enemyLayerMask);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EntityHealth>().SetDamage(_damageMelleAttack, gameObject);
                Debug.Log(enemy.name);
            }
        }

        public void SpawnProjectile()
        {
            if (CurrentAmmo > 0)
            {
                CurrentAmmo -= 1;

                OnSpawnRangeProjectile?.Invoke();

                Debug.Log("range projectile spawned");
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

        protected void OnAttack()
        {
            if (_attackReady)
            {
                _animator.SetTrigger(EntityAnimatorParameters.ATTACK);
            }
        }

        protected void ReloadAmmo()
        {
            CurrentAmmo = _maxAmmo;
        }
    }
}