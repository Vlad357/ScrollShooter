using System;
using UnityEngine;

namespace ScrollShooter
{
    public class RangeProjectile : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private GameObject owner;
        
        [SerializeField] private float damagePoints;
        [SerializeField] private float speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void ParametersInit(float axis, GameObject owner)
        {
            this.owner = owner;
            Vector2 force = new Vector2(axis, 0) * speed;
            _rigidbody.AddForce(force, ForceMode2D.Force);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == owner)
            {
                return;
            }
            if (other.gameObject.TryGetComponent<EntityHealth>(out EntityHealth enemy))
            {
                enemy.SetDamage(damagePoints, owner);
            }
            Destroy(gameObject);
        }
    }

}