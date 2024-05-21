namespace ScrollShooter.EntityScripts
{
    public struct EntityStats
    {
        public float currentHealth;
        public float maxHealth;

        public float melleAttackRadius;
        public float damageMelleAttack;

        public int currentAmmo;
        public int maxAmmo;

        public EntityStats(float current_Health, float max_Health, float melle_Attack_Radius, float damage_Melle_Attack, int current_Ammo, int max_Ammo)
        {
            currentHealth = current_Health;
            maxHealth = max_Health;
            melleAttackRadius = melle_Attack_Radius;
            damageMelleAttack = damage_Melle_Attack;
            currentAmmo = current_Ammo;
            maxAmmo = max_Ammo;
        }

        public static EntityStats operator +(EntityStats a, EntityStats b)
        {
            return new EntityStats(a.currentHealth + b.currentHealth, 
                a.maxHealth + b.maxHealth, 
                a.melleAttackRadius + b.melleAttackRadius,
                a.damageMelleAttack + b.damageMelleAttack,
                a.currentAmmo + b.currentAmmo,
                a.maxAmmo + b.maxAmmo);
        }
    }
}