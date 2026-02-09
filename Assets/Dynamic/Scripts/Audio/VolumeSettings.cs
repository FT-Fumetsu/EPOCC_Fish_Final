using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio.Settings
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _gameMixer;
        [SerializeField] private Slider _environmentVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _masterVolumeSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("EnvironmentVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetEnvironmentVolume();
                SetMasterVolume();
                SetSFXVolume();
            }
        }

        public void SetEnvironmentVolume()
        {
            var volume = _environmentVolumeSlider.value;
            _gameMixer.SetFloat("Environment", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("EnvironmentVolume", volume);
        }

        public void SetSFXVolume()
        {
            var volume = _sfxVolumeSlider.value;
            _gameMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        public void SetMasterVolume()
        {
            var volume = _masterVolumeSlider.value;
            _gameMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        private void LoadVolume()
        {
            _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            _environmentVolumeSlider.value = PlayerPrefs.GetFloat("EnvironmentVolume");
            _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

            SetMasterVolume();
            SetEnvironmentVolume();
            SetSFXVolume();
        }
    }
}