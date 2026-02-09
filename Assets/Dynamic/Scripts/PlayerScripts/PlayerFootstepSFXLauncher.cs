using System;
using Audio;
using UnityEngine;

public class PlayerFootstepSFXLauncher : MonoBehaviour
{
    [SerializeField] private SoundData[] _clips;
    [SerializeField] private Animator _animator;

    private float _lastFootstep;

    private void Awake()
    {
        if (!_animator) _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var footstep = _animator.GetFloat("Footstep");

        if (_lastFootstep > 0 && footstep < 0 || _lastFootstep < 0 && footstep > 0)
        {
            var randomClip = _clips[UnityEngine.Random.Range(0, _clips.Length)];
            AudioHandler.Instance.PlaySound(randomClip);
            Debug.Log("Footstep !");
        }
        
        _lastFootstep = footstep;
    }
}
