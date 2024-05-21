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
    }
}