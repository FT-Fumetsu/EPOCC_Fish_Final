using System;
using UnityEngine;

namespace Fish.StateMachine
{
    public class FishStateMachine : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField, Tooltip("La scène dans laquelle les poissons ne peuvent plus être touchés")] 
        private string _sceneName;
        
        [Header("Speed")]
        [SerializeField] private float _spawnSpeed;
        [SerializeField] private float _constantSpeed;
        [SerializeField] private float _fleeSpeed;
        
        [Header("FleeDirection")]
        [SerializeField] private float _fleeMinAngle = -45f;
        [SerializeField] private float _fleeMaxAngle = 45f;
        
        [Header("Timers")] 
        [SerializeField] private float _stopTimer;
        [SerializeField] private float _timerBeforeStop;

        private IState _currentState;
        private FishStateMachineData _fishStateMachineData;
        
        void Start()
        {
            _fishStateMachineData = new FishStateMachineData()
            {
                FishTransform = transform,
                SpawnSpeed = _spawnSpeed,
                ConstantSpeed = _constantSpeed,
                FleeSpeed =  _fleeSpeed,
                FleeMinAngle = _fleeMinAngle,
                FleeMaxAngle = _fleeMaxAngle,
                StopTimer = _stopTimer,
                TimerBeforeStop = _timerBeforeStop
            };

            _currentState = new FishSpawnState();
            _currentState.Enter(_fishStateMachineData);
        }
    
        void Update()
        {
            #if UNITY_EDITOR
            _fishStateMachineData = new FishStateMachineData()
            {
                FishTransform = transform,
                SpawnSpeed = _spawnSpeed,
                ConstantSpeed = _constantSpeed,
                FleeSpeed =  _fleeSpeed,
                StopTimer = _stopTimer,
                TimerBeforeStop = _timerBeforeStop
            };
            #endif
            
            IState newState = _currentState.Update(_fishStateMachineData);
            if (newState != null)
            {
                _currentState.Exit(_fishStateMachineData);
                _currentState = newState;
                _currentState.Enter(_fishStateMachineData);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(this);
        }
    }
}