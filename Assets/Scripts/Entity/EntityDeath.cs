using ScrollShooter.Supports;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    [RequireComponent(typeof(Entity))]
    public class EntityDeath : MonoBehaviour
    {
        private Entity _entity;

        public GameObject DamageDealer { get; private set; }

        private float _scoreEntity = 10f;

        private void Start()
        {
            _entity = GetComponent<Entity>();
            _entity.OnCheckHealth += CheckCurrentHealth;
            _entity.OnSetDamageDealer += SetDamageDealer;
        }

        private void CheckCurrentHealth(EntityStats stats)
        {
            if(stats.currentHealth <= 0)
            {
                SetScoreOnDeath(DamageDealer);
            }
        }

        private void SetScoreOnDeath(GameObject killer)
        {
            killer.TryGetComponent(out ScoreCounter counter);
            counter?.SetScore(_scoreEntity);
        }

        private void SetDamageDealer(GameObject damageDealer)
        {
            DamageDealer = damageDealer;
        }
    }
}