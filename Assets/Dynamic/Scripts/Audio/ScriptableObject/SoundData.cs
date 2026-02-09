using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
    public class SoundData : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField, Range(0, 2)] private float _volume = 1f;
        [SerializeField, Range(0.5f, 1.5f)] private float _minPitch = 0.9f;
        [SerializeField, Range(1, 2)] private float _maxPitch = 1.5f;
        [SerializeField] private AudioType _audioType = AudioType.SFX;

        public AudioClip Clip => _clip;
        public float Volume => _volume;
        public float MinPitch => _minPitch;
        public float MaxPitch => _maxPitch;
        public AudioType AudioType => _audioType;
    }
}
