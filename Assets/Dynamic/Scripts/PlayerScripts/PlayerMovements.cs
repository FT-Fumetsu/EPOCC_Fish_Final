using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof (Collider))]
    public class PlayerMovements : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Mobile Controls")]
        [SerializeField] private FixedJoystick _joystick;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Debug.Log(_rigidbody.linearVelocity);
            
            float horizontalMovement = _joystick.Horizontal * _moveSpeed;
            float verticalMovement = _joystick.Vertical * _moveSpeed;

            _rigidbody.linearVelocity = new Vector3(horizontalMovement, _rigidbody.linearVelocity.y, verticalMovement);

            if (_joystick.Horizontal != 0f || _joystick.Vertical != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z));
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }
    }
}