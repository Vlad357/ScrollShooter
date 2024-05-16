using ScrollShooter.Supports;
using UnityEngine;

namespace ScrollShooter.Entity
{
    [RequireComponent(typeof(EntityHealth))]
    public class Entity : MonoBehaviour, IBuffable
    {
        private float _scoreEntity = 10f;
        private EntityHealth _entityHealth;
        [SerializeField]private EntityAttack _entityAttack;

        private EntityStats _entityStats;

        public EntityStats EntityCurrentStats
        {
            get { return _entityStats; }
            set
            {
                _entityStats = value;
                _entityHealth.Health = _entityStats.currentHealth;
                _entityHealth.HealthMax = _entityStats.maxHealth;

                _entityAttack.CurrentAmmo = _entityStats.currentAmmo;
                _entityAttack.MaxAmmo = _entityStats.maxAmmo;
            }
        }

        private void Start()
        {
            try
            {
                _entityAttack = GetComponent<EntityAttack>();
            }
            catch
            {
                Debug.Log($"Component entity attack on object {gameObject.name} not found");
            }

            _entityHealth = GetComponent<EntityHealth>();

            _entityAttack.OnSetCurrentAmmo += SetStats;

            _entityHealth.deathEvent += SetScoreOnDeath;
            _entityHealth.OnSetDamage += SetStats;

            EntityCurrentStats = new EntityStats()
            {
                maxHealth = 100,
                currentHealth = 100,
                maxAmmo = 10,
                currentAmmo = 10 
            };
        }

        private void SetStats(EntityStats stats)
        {
            EntityCurrentStats = stats + _entityStats;
        }

        private void SetScoreOnDeath(GameObject killer)
        {
            killer.TryGetComponent(out ScoreCounter counter);
            counter.SetScore(_scoreEntity);
            print(killer.gameObject.name);
        }

        public void ApplyBaff(IBuff buff)
        {
            EntityCurrentStats = buff.ApplyStats(EntityCurrentStats);
        }
    }
}