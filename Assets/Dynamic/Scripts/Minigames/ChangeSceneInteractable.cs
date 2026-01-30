using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactables.Scenes
{
    [RequireComponent(typeof(Collider))]
    public class ChangeSceneInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _sceneToLoad;
        public void OnInteract()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}