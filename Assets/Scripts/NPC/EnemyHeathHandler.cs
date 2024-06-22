using ScroolShooter.Supports;
using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyHeathHandler : EntityHealthHandler
    {
        public event Action<GameObject> SetDamageEventOnTarget;

        public SimpleTimer attackTimer;

        [SerializeField] private float _stunTime;

        public override void SetDamage(float damage, GameObject damageDealer)
        {
            base.SetDamage(damage, damageDealer);

            SetDamageEventOnTarget?.Invoke(damageDealer);
        }

        protected override void TakeHit()
        {
            base.TakeHit();

            attackTimer.SetTargetTime(_stunTime);
            attackTimer.Restart();
        }
    }
}