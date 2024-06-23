using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyMoveAttackHandler : MonoBehaviour
    {
        public event Action OnAttack;
        public event Action<bool> OnMovementReady;

        private int _axisMin = -1, _axisMax = 1;

        private EnemyMovement _enemyMovement;
        private Enemy _enemy;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemy = GetComponent<Enemy>();

            _enemyMovement.OnMovement += EnemyOnMove;
        }

        private void EnemyOnMove()
        {
            if (_enemy.Target == null) return;

            float distance = Vector2.Distance(_enemy.Target.transform.position ,transform.position);
            bool distanceCheck = distance < _enemy.EntityCurrentStats.attackRadius;

            float direction = _enemy.Target.transform.position.x - transform.position.x;
            direction = direction > 0 ? _axisMax : _axisMin;
            bool trueDirection = direction == transform.localScale.x;

            if (distanceCheck)
            {
                OnAttack?.Invoke();
            }

            OnMovementReady?.Invoke(!distanceCheck || !trueDirection);
        }
    }
}