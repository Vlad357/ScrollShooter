using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class RangeEnemy : Enemy
    {
        protected override void InitEntityStats()
        {
            EntityCurrentStats = new EntityStats()
            {
                maxHealth = EntityStatsConfig.MAX_HEALTH,
                currentHealth = EntityStatsConfig.START_HEALTH,
                attackRadius = EntityStatsConfig.RANGE_ATTACK_RADIUS,
                damageMelleAttack = EntityStatsConfig.DAMAGE_MELLE_ATTACK,
                maxAmmo = EntityStatsConfig.MAX_AMMO,
                currentAmmo = EntityStatsConfig.START_AMMO
            };
        }

    }
}