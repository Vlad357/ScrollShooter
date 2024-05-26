using ScroolShooter.Supports;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyHeathHandler : EntityHealthHandler
    {
        public SimpleTimer attackTimer;

        [SerializeField] private float _stunTime;

        protected override void TakeHit()
        {
            base.TakeHit();

            attackTimer.SetTargetTime(_stunTime);
            attackTimer.Restart();
        }
    }
}