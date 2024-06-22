using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyAggravated : MonoBehaviour
    {
        public event Action<GameObject> SetAggravatedTarget;
        public event Action OnUnaggrevated;

        private Animator _animator;
        private EntityDeath _entityDeath;
        private EnemyHeathHandler _enemyHealthHandler;

        [SerializeField]private GameObject _detectTarget;

        private bool IsAggrable = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _entityDeath = GetComponent<EntityDeath>();
            _enemyHealthHandler = GetComponent<EnemyHeathHandler>();

            _entityDeath.OnEntityDeath += () => { 
                SetAggrable(false); 
            };
            _enemyHealthHandler.SetDamageEventOnTarget += OnSetTarget;
        }

        public void OnAggravated()
        {
            SetAggravatedTarget?.Invoke(_detectTarget);
        }

        public void OnSetTarget(GameObject target)
        {
            if (target == null)
            {
                _detectTarget = null;
                OnUnaggrevatedEvent();
                return;
            }

            if(_detectTarget != null)
            {
                return;
            }

            _detectTarget = target;
            _animator.SetTrigger(EnemyAnimatorParameters.AGGRAVATED);
        }

        private void OnUnaggrevatedEvent()
        {
            OnUnaggrevated?.Invoke();
        }

        private void SetAggrable(bool value)
        {
            IsAggrable = value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && IsAggrable)
            {
                OnSetTarget(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnSetTarget(null);
            }
        }
    }
}