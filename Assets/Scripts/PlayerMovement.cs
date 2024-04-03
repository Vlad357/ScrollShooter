using System;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GameplayInputManager))]
    [RequireComponent(typeof(Animator))]

    public class PlayerMovement : MonoBehaviour
    {
        public LayerMask groundLayer;
        
        private GameplayInputManager _gameplayInput;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        [SerializeField] private float jumpForse;
        [SerializeField] private float gruoundJumpOffset = 1.1f;
        [SerializeField]private float speed = 1f;
        private void Start()
        {
            _gameplayInput = GetComponent<GameplayInputManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            
            InitGameplayInput(_gameplayInput);
        }

        private void InitGameplayInput(GameplayInputManager gameplayInputManager)
        {
            gameplayInputManager.MovementPlayerAxisReceived += OnMovementPlayerAxisReceived;
            gameplayInputManager.JumpEventRecived += OnJumpEventRecived;
        }

        private void OnJumpEventRecived()
        {
            if (!InAirCheck())
            {
                return;
            }
            _rigidbody2D.AddForce(Vector2.up * jumpForse, ForceMode2D.Impulse);
            _animator.SetTrigger(PlayerAnimatorParameters.JUMP);
        }

        private void OnMovementPlayerAxisReceived(float axisValue)
        {
            //if (!InAirCheck())
            //{
            //    return;
            //}
            
            InAirCheck();
            
            if (axisValue != 0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(Convert.ToInt32(axisValue), localScale.y, localScale.z);
                transform.localScale = localScale;
            }
            _animator.SetInteger(PlayerAnimatorParameters.SPEED_PERSON, Convert.ToInt32(axisValue));
            axisValue *= (Time.fixedDeltaTime * speed);
            Vector2 moveDirection = new Vector2(axisValue, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = moveDirection;
        }

        private bool InAirCheck()
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                Vector2.down, 
                transform.localScale.y * gruoundJumpOffset,
                groundLayer);

            bool inAir = (hit.collider != null);
            
            _animator.SetBool(PlayerAnimatorParameters.IN_AIR, !inAir);

            return inAir;
        }
    }
}