using UnityEngine;  
using UnityEngine.SceneManagement;

public class RunMinigame : MonoBehaviour
{
    public string minigameSceneName;
    
    public AudioClip runSfx;
    [Range(0, 1)] public float sfxVolume = 1f;
    public void LoadMinigame()
    {
        if (runSfx != null)
        {
            var pos = Camera.main != null ? Camera.main.transform.position : transform.position;
            PlaySFX.PlayClipAtPosition(runSfx, pos, Mathf.Clamp01(sfxVolume), true, true);
        }
        
        SceneManager.LoadScene(minigameSceneName);
    }
    
    public void SaveGame()
    {
        Save.SaveSystem.Instance?.Save();
    }
}
