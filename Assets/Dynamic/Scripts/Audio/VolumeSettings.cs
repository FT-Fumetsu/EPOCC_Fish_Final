using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    private void Start()
    {
        var audioSettingsStore = AudioSettingsStore.Instance;
        if (audioSettingsStore == null)
            return;

        if (musicSlider != null)
        {
            musicSlider.value = audioSettingsStore.MusicVolume;
            musicSlider.onValueChanged.RemoveAllListeners();
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = audioSettingsStore.SfxVolume;
            sfxSlider.onValueChanged.RemoveAllListeners();
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        if (masterSlider != null)
        {
            masterSlider.value = audioSettingsStore.MasterVolume;
            masterSlider.onValueChanged.RemoveAllListeners();
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    public void SetMusicVolume(float value)
    {
        AudioSettingsStore.Instance?.SetMusicVolume(value);
    }

    public void SetSfxVolume(float value)
    {
        AudioSettingsStore.Instance?.SetSfxVolume(value);
    }

    public void SetMasterVolume(float value)
    {
        AudioSettingsStore.Instance?.SetMasterVolume(value);
    }
}