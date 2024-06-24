using ScrollShooter.EntityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollShooter.SecondaryMechanisms
{
    public class Buff : MonoBehaviour, IBuff
    {

        public AudioSource audioSource;

        [Header("buffs")]
        public float health;
        public float maxHealth;
        public int ammo;
        public int maxAmmo;

        public EntityStats ApplyStats(EntityStats baseStats)
        {
            EntityStats newStats = new EntityStats { 
                currentHealth = health,
                maxHealth = maxHealth,
                currentAmmo = ammo,
                maxAmmo = maxAmmo
            };

            baseStats += newStats;
            return baseStats;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.TryGetComponent( out IBuffable buffable))
            {
                buffable.ApplyBaff(this);
                Destroy(gameObject);
                audioSource.Play();
            }
        }
    }
}