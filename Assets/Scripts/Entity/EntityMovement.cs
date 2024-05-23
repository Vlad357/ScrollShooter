using ScrollShooter.Input;
using System;
using UnityEngine;

namespace ScrollShooter.EntityScripts
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] protected LayerMask groundLayer;

        protected Rigidbody2D _rigidbody2D;
        protected Animator _animator;
        protected Entity _entity;

        protected bool _jumpStarted = false, _inAirStarted = false;
        
        protected float jumpForse = 10f;
        protected float _gruoundJumpOffset = 1.6f;
        protected float _gruoundJumpSideOffset = 1.6f;
        
        protected float _groundJumpSideDirectionX = 0.25f;
        
        protected float speed = 200f;
        private bool _freezeMovement = false;

        private void Start()
        {
            Init();
        }

        public void StartJump()
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForse, ForceMode2D.Impulse);

            _jumpStarted = true;
        }

        private void FreezeMovement()
        {
            _freezeMovement = true;
        }


        protected virtual void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _entity = GetComponent<Entity>();

            _entity.OnDeath += FreezeMovement;
        }


        protected void OnJumpEventRecived()
        {
            if (!InAirCheck())
            {
                return;
            }
            _animator.SetTrigger(EntityAnimatorParameters.JUMP);
        }

        protected virtual void OnMovementEntityAxisReceived(float axisValue)
        {
            if (_freezeMovement) return;

            InAirCheck();

            if (axisValue != 0)
            {
                var localScale = transform.localScale;
                localScale = new Vector3(Convert.ToInt32(axisValue), localScale.y, localScale.z);
                transform.localScale = localScale;
            }
            _animator.SetInteger(EntityAnimatorParameters.SPEED_PERSON, Convert.ToInt32(axisValue));
            axisValue *= (Time.fixedDeltaTime * speed);
            Vector2 moveDirection = new Vector2(axisValue, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = moveDirection;
        }

        protected bool InAirCheck()
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
                _animator.SetTrigger(EntityAnimatorParameters.IN_AIR_START);
            }

            if (onGround && (_jumpStarted || _inAirStarted) & _rigidbody2D.velocity.y < 0)
            {
                _jumpStarted = false;
                _inAirStarted = false;
            }

            _animator.SetFloat(EntityAnimatorParameters.FLY_SPEED, _rigidbody2D.velocity.y);
            _animator.SetBool(EntityAnimatorParameters.IN_AIR, !onGround);

            return onGround;
        }
    }
}