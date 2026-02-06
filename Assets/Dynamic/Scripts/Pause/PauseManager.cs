using Singletons;
using UnityEngine;

namespace Manager.Pause
{
    public class PauseManager : PersistentMonoSingleton<PauseManager>
    {
        public bool IsPaused { get; private set; }
        
        public void TogglePause(bool pauseStatus)
        {
            IsPaused = pauseStatus;
            Time.timeScale = IsPaused ? 0f : 1f;
        }
    }
}