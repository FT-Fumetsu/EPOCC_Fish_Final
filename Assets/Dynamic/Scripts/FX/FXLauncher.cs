using UnityEngine;

namespace FX
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">as effect</typeparam>
    public abstract class FXLauncher<T> : MonoBehaviour
    {
        protected T _fx;

        protected virtual void Awake()
        {
            if (_fx != null)
                return;
            
            _fx = GetComponent<T>();
        }

        protected abstract void PlayFX();
    }
}