using UnityEngine;

namespace Fish.StateMachine
{
    public class FishStateMachineData : IStateMachineData
    {
        public Transform FishTransform;

        public float SpawnSpeed;
        public float ConstantSpeed;
        public float FleeSpeed;
    
        public float StopTimer;
        public float TimerBeforeStop;
    }
}