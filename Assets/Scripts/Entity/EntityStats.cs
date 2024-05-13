namespace ScrollShooter.Entity
{
    public struct EntityStats
    {
        public float currentHealth;
        public float maxHealth;

        public int currentAmmo;
        public int maxAmmo;

        public EntityStats(float current_Health, float max_Health, int current_Ammo, int max_Ammo)
        {
            currentHealth = current_Health;
            maxHealth = max_Health;
            currentAmmo = current_Ammo;
            maxAmmo = max_Ammo;
        }

        public static EntityStats operator +(EntityStats a, EntityStats b)
        {
            return new EntityStats(a.currentHealth + b.currentHealth, 
                a.maxHealth + b.maxHealth, 
                a.currentAmmo + b.currentAmmo,
                a.maxAmmo + b.maxAmmo);
        }
    }
}