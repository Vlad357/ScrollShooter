using ScrollShooter.Supports;
using System;
using UnityEngine;

namespace ScrollShooter.Entity
{
    public class EntityHealth : MonoBehaviour
    {
        public event Action<GameObject> deathEvent;
        public event Action<EntityStats> OnSetDamage;

        public Bar HP_bar;

        private float _healthCurrent;
        private float _healthMax;

        public float Health
        {
            get { return _healthCurrent; }
            set
            {
                _healthCurrent = value;
                HP_bar?.SetValueBar(_healthCurrent, _healthMax);
            }
        }
        public float HealthMax
        {
            get { return _healthMax; }
            set
            {
                _healthMax = value;
                HP_bar?.SetValueBar(_healthCurrent, _healthMax);
            }
        }

        public void SetDamage(float damage, GameObject damageDealer)
        {
            if (Health > 0 && damage > 0)
            {
                EntityStats entityStats = new EntityStats();
                entityStats.currentHealth = -damage;

                OnSetDamage?.Invoke(entityStats);

                if (Health <= 0)
                {
                    Death(damageDealer);
                }
            }
        }
        
        private void Death(GameObject damageDealer)
        {
            deathEvent?.Invoke(damageDealer);
        }
    }
}