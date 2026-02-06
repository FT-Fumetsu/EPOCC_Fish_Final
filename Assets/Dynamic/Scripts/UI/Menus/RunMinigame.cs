using UnityEngine;  
using UnityEngine.SceneManagement;

public class RunMinigame : MonoBehaviour
{
    public string minigameSceneName;
    public void LoadMinigame()
    {
        SceneManager.LoadScene(minigameSceneName);
    }
}
