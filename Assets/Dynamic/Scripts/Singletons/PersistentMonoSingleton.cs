using UnityEngine;

namespace Singletons
{
    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : MonoSingleton<T>
    {
        [SerializeField] private bool _isUnparentOnAwake = true;

        protected override void OnInitializing()
        {
            if (_isUnparentOnAwake)
                transform.SetParent(null);
            
            base.OnInitializing();
            if (Application.isPlaying)
                DontDestroyOnLoad(gameObject);
        }
    }
}