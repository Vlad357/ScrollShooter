using ScrollShooter.EntityScripts;
using UnityEngine;
using UnityEngine.Events;

namespace ScrollShooter.Player {
    public class PlayerDeath : EntityDeath
    {
        public override void Death()
        {
            TryGetComponent(out Rigidbody2D rigidbody2D);
            TryGetComponent(out BoxCollider2D boxCollider2D);
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            boxCollider2D.enabled = false;
        }
    }
}