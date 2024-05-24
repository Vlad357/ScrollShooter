namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyAttack : EntityAttack
    {
        private EnemyMoveAttackHandler _enemyMoveAttackHandler;

        protected override void Init()
        {
            base.Init();
            _enemyMoveAttackHandler = GetComponent<EnemyMoveAttackHandler>();
            _enemyMoveAttackHandler.OnAttack += OnAttack;
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            collision.gameObject.TryGetComponent(out EntityHealthHandler health);
            if (health == null) return;

            SetDamageOnEntityHealthHandler(health);
        }
    }
}