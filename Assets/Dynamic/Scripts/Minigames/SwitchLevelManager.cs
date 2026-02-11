using System;
using UnityEngine;

public class SwitchLevelManager : MonoBehaviour
{
    public event Action OnLevelSwitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwitchLevel()
    {
        OnLevelSwitch?.Invoke();
    }
}
