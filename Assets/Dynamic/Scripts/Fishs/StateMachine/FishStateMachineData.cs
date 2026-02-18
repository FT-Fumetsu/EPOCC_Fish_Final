using UnityEngine;

namespace Fish.StateMachine
{
    public class FishStateMachineData : IStateMachineData
    {
        public Transform FishTransform;

        public float SpawnSpeed;
        public float FleeSpeed;
    
        public float FleeMinAngle;
        public float FleeMaxAngle;
        
        public float StopTimer;
        public float TimerBeforeStop;
    }
}