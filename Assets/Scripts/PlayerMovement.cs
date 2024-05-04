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

        private bool _jumpStarted = false, _inAirStarted = false;
        
        [SerializeField] private float jumpForse;
        [SerializeField] private float _gruoundJumpOffset = 1.2f;
        [SerializeField] private float _gruoundJumpSideOffset = 1.03f;
        
        [SerializeField] private float _groundJumpSideDirectionX = 0.55f;
        
        [SerializeField]private float speed = 1f;

        public void StartJump()
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForse, ForceMode2D.Impulse);
            
            _jumpStarted = true;
        }
        
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
            _animator.SetTrigger(PlayerAnimatorParameters.JUMP);
        }

        private void OnMovementPlayerAxisReceived(float axisValue)
        {
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
            float rayMultiplayerDown = transform.localScale.y * _gruoundJumpOffset;
            float rayMultiplayerSide = transform.localScale.y * _gruoundJumpSideOffset;
            
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                Vector2.down, 
                rayMultiplayerDown,
                groundLayer);

            Vector2 sideCheckLeft = Vector2.down + Vector2.left * _groundJumpSideDirectionX;
            Vector2 sideCheckRight = Vector2.down + Vector2.right * _groundJumpSideDirectionX;
            
            RaycastHit2D hitLeft = Physics2D.Raycast(
                transform.position, 
                sideCheckLeft, 
                rayMultiplayerSide,
                groundLayer);
            
            RaycastHit2D hitRight = Physics2D.Raycast(
                transform.position, 
                sideCheckRight, 
                rayMultiplayerSide,
                groundLayer);

            Debug.DrawRay(transform.position, Vector2.down * rayMultiplayerDown, Color.green);
            Debug.DrawRay(transform.position, sideCheckLeft * rayMultiplayerSide, Color.green);
            Debug.DrawRay(transform.position, sideCheckRight * rayMultiplayerSide, Color.green);
            
            bool sideHit = hitLeft.collider != null || hitRight.collider != null;
            bool onGround = sideHit || hit.collider != null;

            if (!onGround && !_jumpStarted && !_inAirStarted)
            {
                _inAirStarted = true;
                _animator.SetTrigger(PlayerAnimatorParameters.IN_AIR_START);
                print("in air start");
            }

            if (onGround && (_jumpStarted || _inAirStarted) & _rigidbody2D.velocity.y < 0)
            {
                _jumpStarted = false;
                _inAirStarted = false;
            }
            
            _animator.SetFloat(PlayerAnimatorParameters.FLY_SPEED, _rigidbody2D.velocity.y);
            _animator.SetBool(PlayerAnimatorParameters.IN_AIR, !onGround);
            
            return onGround;
        }
    }
}