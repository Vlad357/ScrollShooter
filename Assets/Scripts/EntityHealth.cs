using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScrollShooter
{
    public class EntityHealth
    {
        public event Action<GameObject> deathEvent;
        
        private float healthCurrent;
        private float _healthMax = 100;
        
        public void SetDamage(float damage, GameObject damageDealer)
        {
            if (healthCurrent > 0 && damage > 0)
            {
                healthCurrent -= damage;
                if (healthCurrent <= 0)
                {
                    deathEvent?.Invoke(damageDealer);
                }
            }
        }
        
        public void Regeneration()
        {
            healthCurrent = _healthMax;
        }
    }
}