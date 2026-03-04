using Singletons;
using UnityEngine;

namespace Audio
{
    public class MusicManager : PersistentMonoSingleton<MusicManager>
    {
        [SerializeField] private AudioSource _musicSource;

        [SerializeField] private AudioClip _globalMusic;

        // Start is called once before the first execution of Update after the MonoBehaviour is 
        private void Start()
        {
            _musicSource.clip = _globalMusic;
            _musicSource.Play();
        }
    }
}