using ScrollShooter.SecondaryMechanisms;
using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    [RequireComponent(typeof(EntityHealthHandler))]
    [RequireComponent(typeof(EntityAttack))]
    public class Entity : MonoBehaviour, IBuffable
    {
        public event Action<EntityStats> OnCheckHealth;
        public event Action<GameObject> OnSetDamageDealer;

        public event Action<float, float> OnSetHealthView;
        public event Action<float, float> OnSetAmmoView;

        public event Action OnDeath;

        private EntityHealthHandler _entityHealth;
        private EntityAttack _entityAttack;
        private EntityDeath _entityDeath;

        private EntityStats _entityStats;

        public EntityStats EntityCurrentStats
        {
            get { return _entityStats; }
            private set
            {
                _entityStats = value;
                OnSetHealthView?.Invoke(_entityStats.currentHealth, _entityStats.maxHealth);
                OnSetAmmoView?.Invoke(_entityStats.currentAmmo, _entityStats.maxAmmo);
            }
        }

        private void Start()
        {
            Init();
        }

        public void ApplyBaff(IBuff buff)
        {
            EntityCurrentStats = buff.ApplyStats(EntityCurrentStats);
        }

        private void SetStats(EntityStats stats)
        {
            EntityCurrentStats = stats + _entityStats;
            if(stats.currentHealth != 0)
            {
                print(EntityCurrentStats.currentHealth);
                OnCheckHealth?.Invoke(EntityCurrentStats);
            }
        }

        private void SetDamageDealer(GameObject damageDealer)
        {
            OnSetDamageDealer?.Invoke(damageDealer);
        }

        private void OnDeathEvent()
        {
            OnDeath?.Invoke();
        }

        protected virtual void Init()
        {
            _entityAttack = GetComponent<EntityAttack>();
            _entityHealth = GetComponent<EntityHealthHandler>();
            _entityDeath = GetComponent<EntityDeath>();

            _entityAttack.OnSetCurrentAmmo += SetStats;

            _entityHealth.OnSetDamage += SetStats;
            _entityHealth.OnSetDamageDealer += SetDamageDealer;

            _entityDeath.OnEntityDeath += OnDeathEvent;

            EntityCurrentStats = new EntityStats()
            {
                maxHealth = EntityStatsConfig.MAX_HEALTH,
                currentHealth = EntityStatsConfig.START_HEALTH,
                melleAttackRadius = EntityStatsConfig.MELLE_ATTACK_RADIUS,
                damageMelleAttack = EntityStatsConfig.DAMAGE_MELLE_ATTACK,
                maxAmmo = EntityStatsConfig.MAX_AMMO,
                currentAmmo = EntityStatsConfig.START_AMMO
            };
        }
    }
}