using Manager.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        public void OpenLevel(string levelName)
        {
            Debug.LogWarning("OPEN LEVEL " + levelName);
            PauseManager.Instance.TogglePause(false);
            SceneManager.LoadScene(levelName);
        }

        public void QuitGame()
        {
            Debug.Log("QUIT GAME !!!");
            Application.Quit();
        }

        public void SaveGame()
        {
            Save.SaveSystem.Instance?.Save();
        }
    }
}