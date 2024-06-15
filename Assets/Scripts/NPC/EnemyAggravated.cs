using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyAggravated : MonoBehaviour
    {
        public event Action<GameObject> SetAggravatedTarget;
        public event Action OnUnaggrevated;

        private Animator _animator;
        private EntityDeath _entityDeath;
        private GameObject _detectTarget;

        private bool IsAggrable = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _entityDeath = GetComponent<EntityDeath>();

            _entityDeath.OnEntityDeath += () => { 
                SetAggrable(false); 
            };
        }

        public void OnAggravated()
        {
            SetAggravatedTarget?.Invoke(_detectTarget);
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
                _animator.SetTrigger(EnemyAnimatorParameters.AGGRAVATED);
                _detectTarget = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnUnaggrevatedEvent();
            }
        }
    }
}