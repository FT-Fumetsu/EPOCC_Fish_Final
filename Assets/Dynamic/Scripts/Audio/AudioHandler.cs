using System;
using Singletons;
using UnityEngine;

namespace Audio
{
    public class AudioHandler : MonoSingleton<AudioHandler>
    {
        [SerializeField] private AudioSource _environmentSource;
        [SerializeField] private AudioSource _sfxSource;

        private IAudioSystem _audioSystem;

        protected override void Awake()
        {
            base.Awake();
            _audioSystem = new AudioSystem();
        }

        public void PlaySound(SoundData data)
        {
            switch (data.AudioType)
            {
                case AudioType.SFX:
                    _audioSystem.Play(data, _sfxSource);
                    break;
                case AudioType.Environment:
                    _audioSystem.Play(data, _environmentSource);
                    break;
            }
        }

        public void StopSound()
        {
            _audioSystem.Stop(_environmentSource);
        }
    }
}