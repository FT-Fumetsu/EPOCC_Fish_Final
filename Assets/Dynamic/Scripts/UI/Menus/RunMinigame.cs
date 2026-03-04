using UnityEngine;  
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RunMinigame : MonoBehaviour
{
    public string minigameSceneName;
    
    public AudioClip runSfx;
    [Range(0, 1)] public float sfxVolume = 1f;
    [Tooltip("Groupe d'AudioMixer utilisé pour les SFX joués par ce script (optionnel)")]
    [SerializeField] private AudioMixerGroup _sfxGroup;
    public void LoadMinigame()
    {
        if (runSfx != null)
        {
            var pos = Camera.main != null ? Camera.main.transform.position : transform.position;
            PlaySFX.PlayClipAtPosition(runSfx, pos, Mathf.Clamp01(sfxVolume), true, true, _sfxGroup);
        }
        
        SceneManager.LoadScene(minigameSceneName);
    }
    
    public void SaveGame()
    {
        Save.SaveSystem.Instance?.Save();
    }
}