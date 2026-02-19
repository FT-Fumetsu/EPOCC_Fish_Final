using System;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;
    
    public Vector3 Direction{ get => _direction; set => _direction = value.normalized; }

    private void Start()
    {
        _direction = transform.right;
    }

    private void Update()
    {
        transform.position += _direction * (_speed * Time.deltaTime);
    }
}
