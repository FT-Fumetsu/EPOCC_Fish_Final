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
        void Start()
        {

        }

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
            float horizontal = _joystick.Horizontal;
            float vertical = _joystick.Vertical;
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveDirection = direction * _moveSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(transform.position + moveDirection);

                // Rotate player towards movement direction
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                _rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);

                //// Set animation parameter
                //_animator.SetBool("isRunning", true);
            }
            //else
            //{
            //    // Set animation parameter
            //    _animator.SetBool("isRunning", false);
            //}

        }
    }
}