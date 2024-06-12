using UnityEngine;
using UnityEngine.Events;

namespace ScrollShooter.Supports
{
    public class TargetEvent : MonoBehaviour
    {
        public UnityEvent onTargetEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                OnTargetEnentCall();
            }
        }

        private void OnTargetEnentCall()
        {
            onTargetEvent.Invoke();
        }
    }
}