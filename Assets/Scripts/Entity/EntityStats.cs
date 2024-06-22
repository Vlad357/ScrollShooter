namespace ScrollShooter.EntityScripts
{
    public struct EntityStats
    {
        public float currentHealth;
        public float maxHealth;

        public float attackRadius;
        public float rangeAttackRadius;
        public float damageMelleAttack;

        public int currentAmmo;
        public int maxAmmo;

        public EntityStats(float current_Health, float max_Health, float melle_Attack_Radius, float range_Attack_Radius, float damage_Melle_Attack, int current_Ammo, int max_Ammo)
        {
            currentHealth = current_Health;
            maxHealth = max_Health;
            attackRadius = melle_Attack_Radius;
            rangeAttackRadius = range_Attack_Radius;
            damageMelleAttack = damage_Melle_Attack;
            currentAmmo = current_Ammo;
            maxAmmo = max_Ammo;
        }

        public static EntityStats operator +(EntityStats a, EntityStats b)
        {
            a.currentHealth += b.currentHealth;
            if(a.currentHealth > a.maxHealth + b.maxHealth)
            {
                a.currentHealth = a.maxHealth + b.maxHealth;
            }
            a.maxHealth += b.maxHealth;
            a.attackRadius += b.attackRadius;
            a.rangeAttackRadius += b.rangeAttackRadius;
            a.damageMelleAttack += b.damageMelleAttack;
            a.currentAmmo += b.currentAmmo;
            a.maxAmmo += b.maxAmmo;

            return a;
        }
    }
}