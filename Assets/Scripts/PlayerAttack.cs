using System;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(GameplayInputManager))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float melleAttackRadius;
        [SerializeField] private LayerMask enemyLayerMask;
        
        private GameplayInputManager _gameplayInputManager;
        private Animator _animator;
        
        private void Start()
        {
            _gameplayInputManager = GetComponent<GameplayInputManager>();
            _animator = GetComponent<Animator>();
            
            _gameplayInputManager.Attack += GameplayInputManagerOnAttack;
        }
        
        public void MelleAttack()
        {
            Debug.Log("melle attack detect enemy");
            
            Collider2D[] enemies = 
                Physics2D.OverlapCircleAll(transform.position, melleAttackRadius, enemyLayerMask);

            foreach (var VARIABLE in enemies)
            {
                Debug.Log(VARIABLE.name);
            }
        }

        public void SpawnProjectile()
        {
            Debug.Log("range projectile spawned");
        }
        
        private void GameplayInputManagerOnAttack()
        {
            print("Attack");
            _animator.SetTrigger(PlayerAnimatorParameters.ATTACK);
        }
    }
}