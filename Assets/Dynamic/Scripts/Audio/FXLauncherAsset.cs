using UnityEngine;

public abstract class FXLauncherAsset<T> : MonoBehaviour where T : ScriptableObject
{
    protected T _fx;

    protected virtual void Awake()
    {
        
    }

    protected abstract void PlayFX();
}