using ScroolShooter.Supports;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyAttack : EntityAttack
    {
        public SimpleTimer attackTimer;

        private EnemyMoveAttackHandler _enemyMoveAttackHandler;

        [SerializeField] private float _attackCoolDownTime = 1f;

        protected override void Init()
        {
            base.Init();
            _enemyMoveAttackHandler = GetComponent<EnemyMoveAttackHandler>();
            _enemyMoveAttackHandler.OnAttack += OnAttack;

            attackTimer.OnTimerEnded += ReadyAttackTurnOn;
        }

        protected override void OnAttack()
        {
            base.OnAttack();

            if (_attackReady)
            {
                attackTimer.SetTargetTime(_attackCoolDownTime);
                attackTimer.Restart();
            }
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            collision.gameObject.TryGetComponent(out EntityHealthHandler health);
            if (health == null) return;

            SetDamageOnEntityHealthHandler(health);
        }
    }
}