using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PlaySFX : MonoBehaviour
{
    [Tooltip("Groupe d'AudioMixer vers lequel ce SFX sera routé (optionnel)")]
    [SerializeField] private AudioMixerGroup _mixerGroup;

    [Tooltip("Clip audio joué par ce component")]
    [SerializeField] private AudioClip _clip;
    [Range(0f, 1f), SerializeField] private float _volume = 1f;
    
    //[SerializeField] private bool _playOnStart = false;
    [SerializeField] private bool _autoDestroy = true;
    [Tooltip("Si vrai, l'auto-destruction utilisera WaitForSecondsRealtime pour ne pas être bloquée par Time.timeScale == 0")]
    [SerializeField] private bool _useRealtimeForDestroy = true;

    private AudioSource _source;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
        _source.spatialBlend = 0f;
        // Assigner le groupe de mixeur (si renseigné) pour permettre le contrôle global du volume
        if (_mixerGroup != null)
            _source.outputAudioMixerGroup = _mixerGroup;
    }

    public void Play()
    {
        if (_clip == null)
        {
            return;
        }

        _source.clip = _clip;
        _source.volume = _volume;
        // (ré)appliquer le groupe au moment de jouer, si on l'a changé via InitAndPlay
        if (_mixerGroup != null)
            _source.outputAudioMixerGroup = _mixerGroup;
        _source.Play();

        if (_autoDestroy)
        {
            var delay = Mathf.Max(0.05f, _clip.length);
            StartCoroutine(AutoDestroyAfter(delay));
        }
    }

    // Ajout d'un paramètre optionnel pour le groupe de mixeur afin de pouvoir l'assigner depuis l'appel statique
    public void InitAndPlay(AudioClip clip, float volume = 1f, bool autoDestroy = true, bool useRealtime = true, AudioMixerGroup mixerGroup = null)
    {
        _clip = clip;
        _volume = volume;
        _autoDestroy = autoDestroy;
        _useRealtimeForDestroy = useRealtime;
        if (mixerGroup != null)
            _mixerGroup = mixerGroup;
        Play();
    }

    private IEnumerator AutoDestroyAfter(float seconds)
    {
        if (_useRealtimeForDestroy)
            yield return new WaitForSecondsRealtime(seconds);
        else
            yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
    
    public static void PlayClipAtPosition(AudioClip clip, Vector3 position, float volume = 1f, bool autoDestroy = true, bool useRealtime = true, AudioMixerGroup mixerGroup = null)
    {
        if (clip == null)
        {
            return;
        }

        var go = new GameObject($"SFX_{clip.name}");
        go.transform.position = position;
        var sfx = go.AddComponent<PlaySFX>();
        sfx.InitAndPlay(clip, volume, autoDestroy, useRealtime, mixerGroup);
    }
}
