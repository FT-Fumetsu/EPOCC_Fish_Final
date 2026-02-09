using UnityEngine;

namespace Audio
{
    public interface IAudioSystem
    {
        public void Play(SoundData data, AudioSource audioSource);
        public void Stop(AudioSource audioSource);
    }
}