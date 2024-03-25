using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GameplayInputManager))]

    public class PlayerMovement : MonoBehaviour
    {
        private GameplayInputManager _gameplayInput;
        private Rigidbody2D _rigidbody2D;

        [SerializeField]private float speed = 1f;
        private void Start()
        {
            _gameplayInput = GetComponent<GameplayInputManager>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            InitGameplayInput(_gameplayInput);
        }

        private void InitGameplayInput(GameplayInputManager gameplayInputManager)
        {
            gameplayInputManager.MovementPlayerAxisReceived += OnMovementPlayerAxisReceived;
        }

        private void OnMovementPlayerAxisReceived(float axisValue)
        {
            axisValue *= (Time.fixedDeltaTime * speed);
            Vector2 moveDirection = new Vector2(axisValue, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = moveDirection;
        }
    }
}