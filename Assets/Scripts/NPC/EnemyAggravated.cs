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
        private GameObject _detectTarget;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnAggravated()
        {
            SetAggravatedTarget?.Invoke(_detectTarget);
        }

        private void OnUnaggrevatedEvent()
        {
            OnUnaggrevated?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
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