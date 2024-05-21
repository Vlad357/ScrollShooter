using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts.Enemy
{
    public class EnemyMovement : EntityMovement
    {
        public event Action OnMovement;

        private Enemy _enemy;
        private EnemyMoveAttackHandler _enemyMoveAttackHandler;

        private bool _isMovingReady;
        private bool _isTargetSet;
        private float _axisMin = -1, _axisMax = 1;

        protected override void Init()
        {
            base.Init();

            _enemy = GetComponent<Enemy>();
            _enemyMoveAttackHandler = GetComponent<EnemyMoveAttackHandler>();

            _enemy.OnSetTarget += TargetOnSet;
            _enemyMoveAttackHandler.OnMovementReady += IsMovingReady;
        }

        private void IsMovingReady(bool value)
        {
            _isMovingReady = value;
            if (!value)
            {
                OnMovementEntityAxisReceived(0);
            }
        }

        private void TargetOnSet(bool value)
        {
            _isTargetSet = value;
        }


        private void FixedUpdate()
        {
            if (_isTargetSet)
            {
                OnMovement?.Invoke();

                if (!_isMovingReady) return;

                float axis = _enemy.Target.transform.position.x - transform.position.x;
                float axisNormalized = Mathf.Clamp(axis, _axisMin, _axisMax);
                OnMovementEntityAxisReceived(axisNormalized);
            }
        }

        protected override void OnMovementEntityAxisReceived(float axisValue)
        {
            base.OnMovementEntityAxisReceived(axisValue);
        }

    }
}