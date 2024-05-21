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

        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;
        private Enemy _enemy;

        private void Start()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _enemy = GetComponent<Enemy>();

            _enemyMovement.OnMovement += EnemyOnMove;
        }

        private void EnemyOnMove()
        {
            float distance = Vector2.Distance(_enemy.Target.transform.position ,transform.position);

            if(distance < _enemy.EntityCurrentStats.melleAttackRadius)
            {
                OnAttack?.Invoke();
            }

            OnMovementReady?.Invoke(distance > _enemy.EntityCurrentStats.melleAttackRadius);
        }
    }
}