using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScrollShooter
{
    public class EntityHealth : MonoBehaviour
    {
        public event Action<GameObject> deathEvent;

        public Bar HP_bar;

        private float healthCurrent;
        private float _healthMax = 100;


        public float Health
        {
            get { return healthCurrent; }
            private set
            {
                healthCurrent = value;
                HP_bar?.SetValueBar(healthCurrent, _healthMax);
            }
        }
        public void SetDamage(float damage, GameObject damageDealer)
        {
            if (Health > 0 && damage > 0)
            {
                Health -= damage;
                if (Health <= 0)
                {
                    Death(damageDealer);
                }
            }
        }
        
        public void Regeneration()
        {
            Health = _healthMax;
        }

        private void Death(GameObject damageDealer)
        {
            deathEvent?.Invoke(damageDealer);
        }
    }
}