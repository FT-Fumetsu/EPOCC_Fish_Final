using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Singletons;

/// <summary>
/// Singleton persistant qui expose les réglages audio et s'occupe de leur sauvegarde/restauration.
/// Stocke les volumes (0..1) et applique les conversions en dB sur l'AudioMixer.
/// </summary>
[DefaultExecutionOrder(-100)]
public class AudioSettingsStore : PersistentMonoSingleton<AudioSettingsStore>
{
    [Tooltip("Référence à l'AudioMixer utilisé pour appliquer les valeurs")]
    [SerializeField] private AudioMixer audioMixer;

    [Tooltip("Nom du paramètre 'music' dans l'AudioMixer")]
    [SerializeField] private string musicParam = "music";
    [Tooltip("Nom du paramètre 'sfx' dans l'AudioMixer")]
    [SerializeField] private string sfxParam = "sfx";
    [Tooltip("Nom du paramètre 'master' dans l'AudioMixer")]
    [SerializeField] private string masterParam = "master";

    private const string MusicKey = "musicVolume";
    private const string SfxKey = "sfxVolume";
    private const string MasterKey = "masterVolume";

    private float music = 1f;
    private float sfx = 1f;
    private float master = 1f;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        LoadAll();
        StartCoroutine(ApplyMixerNextFrame());
    }

    private IEnumerator ApplyMixerNextFrame()
    {
        yield return null;

        ApplyParam(musicParam, music);
        ApplyParam(sfxParam, sfx);
        ApplyParam(masterParam, master);
    }

    private static float VolumeToDecibels(float v)
    {
        v = Mathf.Clamp(v, 0.0001f, 1f);
        return Mathf.Log10(v) * 20f;
    }

    private void ApplyParam(string paramName, float value01)
    {
        if (audioMixer == null) return;
        audioMixer.SetFloat(paramName, VolumeToDecibels(value01));
    }

    public void SetMusicVolume(float value01)
    {
        music = Mathf.Clamp01(value01);
        ApplyParam(musicParam, music);
        PlayerPrefs.SetFloat(MusicKey, music);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float value01)
    {
        sfx = Mathf.Clamp01(value01);
        ApplyParam(sfxParam, sfx);
        PlayerPrefs.SetFloat(SfxKey, sfx);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value01)
    {
        master = Mathf.Clamp01(value01);
        ApplyParam(masterParam, master);
        PlayerPrefs.SetFloat(MasterKey, master);
        PlayerPrefs.Save();
    }

    public float MusicVolume => music;
    public float SfxVolume => sfx;
    public float MasterVolume => master;

    public void LoadAll()
    {
        bool result = audioMixer.SetFloat(musicParam, VolumeToDecibels(music));
        Debug.Log("Set music result: " + result);
        
        music = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfx = PlayerPrefs.GetFloat(SfxKey, 1f);
        master = PlayerPrefs.GetFloat(MasterKey, 1f);

        if (audioMixer != null)
        {
            ApplyParam(musicParam, music);
            ApplyParam(sfxParam, sfx);
            ApplyParam(masterParam, master);
        }
        
        Debug.Log("Music param after set:");
        float test;
        audioMixer.GetFloat(musicParam, out test);
        Debug.Log(test);
    }
}
