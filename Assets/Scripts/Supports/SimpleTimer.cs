using System;
using UnityEngine;

namespace ScroolShooter.Supports
{
    public class SimpleTimer : MonoBehaviour
    {
        public event Action OnTimerEnded;

        [SerializeField] private float _targetTime;
        [SerializeField] private float _currentTime;

        public void SetTargetTime(float targetTime)
        {
            if (targetTime <= 0) return;
            _targetTime = targetTime;
        }

        public void Restart()
        {
            _currentTime = _targetTime;
        }

        private void Update()
        {
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0)
                {
                    TimerEnded();
                }
            }
        }

        private void TimerEnded()
        {
            OnTimerEnded?.Invoke();
        }
    }
}