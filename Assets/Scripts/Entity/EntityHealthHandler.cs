using ScrollShooter.Supports;
using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    public class EntityHealthHandler : MonoBehaviour
    {
        public event Action<EntityStats> OnSetDamage;
        public event Action<GameObject> OnSetDamageDealer;

        private Entity _entity;
        private Animator _animator;

        private void Start()
        {
            _entity = GetComponent<Entity>();
            _animator = GetComponent<Animator>();
        }

        public virtual void SetDamage(float damage, GameObject damageDealer)
        {
            if (_entity.EntityCurrentStats.currentHealth < 0 && damage < 0)
            {
                return;
            }

            EntityStats entityStats = new EntityStats();
            entityStats.currentHealth = -damage;

            TakeHit();

            OnSetDamageDealer?.Invoke(damageDealer);
            OnSetDamage?.Invoke(entityStats);
        }

        protected virtual void TakeHit()
        {
            if (!_animator.GetBool(EntityAnimatorParameters.ATTACK_PROCESS))
            {
                _animator.SetTrigger(EntityAnimatorParameters.TAKE_HIT);
            }
        }
    }
}