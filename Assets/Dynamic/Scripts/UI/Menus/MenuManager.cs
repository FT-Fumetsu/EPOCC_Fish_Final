using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT GAME !!!");
        Application.Quit();
    }
}
