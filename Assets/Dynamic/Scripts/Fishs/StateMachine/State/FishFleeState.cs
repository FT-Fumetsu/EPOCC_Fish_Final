using UnityEngine;

namespace Fish.StateMachine
{
    public class FishFleeState: IState
    {
        public void Enter(IStateMachineData stateMachineData){}
    
        public IState Update(IStateMachineData stateMachineData)
        {
            var data = (FishStateMachineData)stateMachineData;
            data.FishTransform.position += data.FishTransform.right * data.FleeSpeed * Time.deltaTime;
            
            return null;
        }
    
        public void Exit(IStateMachineData stateMachineData){}
    }
}