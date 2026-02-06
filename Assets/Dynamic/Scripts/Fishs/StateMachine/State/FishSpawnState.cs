using UnityEngine;

namespace Fish.StateMachine
{
    public class FishSpawnState: IState
    { 
        private float _timer = 0f;
        
        public void Enter(IStateMachineData stateMachineData){}
    
        public IState Update(IStateMachineData stateMachineData)
        {
            MoveFish(stateMachineData);
            var data = (FishStateMachineData)stateMachineData;
            
            _timer += Time.deltaTime;
            
            if(_timer >= data.TimerBeforeStop)
            {
                return new FishStopState();
            }
            return null;
        }
    
        public void Exit(IStateMachineData stateMachineData){}
        
        private void MoveFish(IStateMachineData stateMachineData)
        {
            var data = (FishStateMachineData)stateMachineData;
            data.FishTransform.position += data.FishTransform.right * data.SpawnSpeed * Time.deltaTime;
        }
    }
}