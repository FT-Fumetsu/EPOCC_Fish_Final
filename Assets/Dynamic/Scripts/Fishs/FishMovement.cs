using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fish.Movement
{
    public class FishMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private Vector3 _direction = Vector3.right;
        void Update()
        {
            transform.Translate(_direction.normalized * _moveSpeed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);    
        }
    }
}