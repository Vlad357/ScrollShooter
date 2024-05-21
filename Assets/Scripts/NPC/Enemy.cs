using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace ScrollShooter.EntityScripts.Enemy
{
    [RequireComponent(typeof(EnemyMoveAttackHandler))]
    [RequireComponent(typeof(EnemyAggravated))]
    public class Enemy : Entity
    {
        public event Action<bool> OnSetTarget;

        private EnemyAggravated _enemyAggravated;

        private GameObject _target;

        public GameObject Target
        {
            get
            {
                return _target;
            }

            private set
            {
                _target = value;
            }
        }

        protected override void Init()
        {
            base.Init();

            _enemyAggravated = GetComponent<EnemyAggravated>();

            _enemyAggravated.SetAggravatedTarget += OnAggravatedTarget;
        }


        private void OnAggravatedTarget(GameObject target)
        {
            _target = target;

            OnSetTarget?.Invoke(_target != null);
        }
    }
}