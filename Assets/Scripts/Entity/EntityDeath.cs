using ScrollShooter.Supports;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScrollShooter.EntityScripts
{
    [RequireComponent(typeof(Entity))]
    public class EntityDeath : MonoBehaviour
    {
        public event Action OnEntityDeath;

        private Entity _entity;
        private Animator _animator;

        public GameObject DamageDealer { get; private set; }

        private float _scoreEntity = 10f;

        private void Start()
        {
            _entity = GetComponent<Entity>();
            _animator = GetComponent<Animator>();
            _entity.OnCheckHealth += CheckCurrentHealth;
            _entity.OnSetDamageDealer += SetDamageDealer;
        }

        public virtual void Death()
        {
            Destroy(gameObject);
        }


        private void CheckCurrentHealth(EntityStats stats)
        {
            if(stats.currentHealth <= 0)
            {
                SetScoreOnDeath(DamageDealer);
                _animator.SetTrigger(EntityAnimatorParameters.DEATH);
                OnEntityDeath?.Invoke();
            }
        }

        private void SetScoreOnDeath(GameObject killer)
        {
            killer.TryGetComponent(out ScoreCounter counter);
            counter?.SetScore(_scoreEntity);
        }

        private void SetDamageDealer(GameObject damageDealer)
        {
            DamageDealer = damageDealer;
        }
    }
}