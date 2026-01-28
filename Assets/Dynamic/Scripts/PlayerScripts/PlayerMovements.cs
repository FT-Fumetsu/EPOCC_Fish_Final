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
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void FixedUpdate()
        {
            Move();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Move()
        {
            float horizontalMovement = _joystick.Horizontal * _moveSpeed;
            float verticalMovement = _joystick.Vertical * _moveSpeed;

            _rigidbody.linearVelocity = new Vector3(horizontalMovement, _rigidbody.angularVelocity.y, verticalMovement);

            if (_joystick.Horizontal != 0f || _joystick.Vertical != 0f)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity);
                //_animator.SetBool("isRunning", true);
            }
            //else
            //{
            //    _animator.SetBool("isRunning", false);
            //}
        }
    }
}