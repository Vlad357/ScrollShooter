using ScrollShooter.Entity;
using System.Collections;
using UnityEngine;

namespace ScrollShooter.SecondaryMechanisms
{
    public class HazardousAreas : MonoBehaviour
    {
        private float tickTime = 0.5f;
        private float damage = 2f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject collisionObject = collision.gameObject;
            if (collisionObject.CompareTag("Player"))
            {
                EntityHealth health = collisionObject.GetComponent<EntityHealth>();
                StartCoroutine(TickDamage(health));
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator TickDamage(EntityHealth health)
        {
            health.SetDamage(damage, gameObject);
            yield return new WaitForSeconds(tickTime);
            StartCoroutine(TickDamage(health));
        }
    }
}