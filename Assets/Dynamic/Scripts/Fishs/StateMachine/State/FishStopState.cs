using UnityEngine;

namespace Fish.StateMachine
{
    public class FishStopState: IState
    {
        private float _timer = 0f;
        
        public void Enter(IStateMachineData stateMachineData)
        {
        }
    
        public IState Update(IStateMachineData stateMachineData)
        {
            var data = (FishStateMachineData)stateMachineData;
            
            _timer += Time.deltaTime;
            
            if (_timer >= data.StopTimer)
            {
                return new FishFleeState();
            }
            return null;
        }
    
        public void Exit(IStateMachineData stateMachineData){}
    }
}