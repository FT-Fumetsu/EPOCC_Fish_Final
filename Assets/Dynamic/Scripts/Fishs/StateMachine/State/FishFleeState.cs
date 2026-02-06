using UnityEngine;

namespace Fish.StateMachine
{
    public class FishFleeState: IState
    {

        public void Enter(IStateMachineData stateMachineData)
        {
            var data = (FishStateMachineData)stateMachineData;
            float fleeRotationAngle = Random.Range(data.FleeMinAngle, data.FleeMaxAngle);
            data.FishTransform.rotation = Quaternion.Euler(0, 0, fleeRotationAngle + data.FishTransform.rotation.eulerAngles.z);
        }
    
        public IState Update(IStateMachineData stateMachineData)
        {
            var data = (FishStateMachineData)stateMachineData;
            data.FishTransform.position += data.FishTransform.right * data.FleeSpeed * Time.deltaTime;
            
            return null;
        }
    
        public void Exit(IStateMachineData stateMachineData){}
    }
}