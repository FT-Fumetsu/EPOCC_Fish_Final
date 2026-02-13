using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactables.Scenes
{
    [RequireComponent(typeof(Collider))]
    public class ChangeSceneInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _changeScene;
        [SerializeField] private string _sceneToLoad;
        public void OnInteract()
        {
            Debug.Log("Interact");
            var runMinigame = _changeScene.GetComponent<RunMinigame>();
            runMinigame.minigameSceneName = _sceneToLoad;
            _changeScene.SetActive(true);
        }
    }
}