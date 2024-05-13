using ScrollShooter.Supports;
using UnityEngine;

namespace ScrollShooter.Entity
{
    [RequireComponent(typeof(EntityHealth))]
    public class Entity : MonoBehaviour
    {
        private float _scoreEntity = 10f;
        private EntityHealth _health;

        private EntityStats _entityStats;

        public EntityStats EntityStats
        {
            get { return _entityStats; }
            set
            {
                _entityStats = value;
                _health.Health = _entityStats.currentHealth;
                _health.HealthMax = _entityStats.maxHealth;

            }
        }

        private void Start()
        {
            _health = GetComponent<EntityHealth>();
            _health.deathEvent += SetScoreOnDeath;
            _health.OnSetDamage += SetStats;

            EntityStats = new EntityStats()
            {
                maxHealth = 100,
                currentHealth = 100,
                maxAmmo = 10,
                currentAmmo = 10 
            };
        }

        private void SetStats(EntityStats stats)
        {
            EntityStats = stats + _entityStats;
            print(EntityStats.currentHealth);
        }

        private void SetScoreOnDeath(GameObject killer)
        {
            killer.TryGetComponent(out ScoreCounter counter);
            counter.SetScore(_scoreEntity);
        }
    }
}