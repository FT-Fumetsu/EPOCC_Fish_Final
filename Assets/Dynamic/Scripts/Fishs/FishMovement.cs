using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fish.Movement
{
    public class FishMovement : MonoBehaviour
    {
        [SerializeField] private float _constantMoveSpeed;

        [SerializeField] private Vector3 _direction = Vector3.right;
         
        [Header("TapeTaupe")]
        [SerializeField] private bool _tapeTaupe = false;

        [SerializeField] private float _beginSpeed;
        [SerializeField] private float _endSpeed;
        [SerializeField] private float _timeBeforeStop;
        [SerializeField] private float _timeStop;
        
        public bool TapeTaupe
        {
            get => _tapeTaupe;
            set => _tapeTaupe = value;
        }
        void Update()
        {
            transform.Translate(_direction.normalized * _constantMoveSpeed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);    
        }
    }
}