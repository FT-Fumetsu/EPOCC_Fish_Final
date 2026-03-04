using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactables.Scenes
{
    [RequireComponent(typeof(Collider))]
    public class ChangeSceneInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _changeScene;
        [SerializeField] private string _sceneToLoad;
        [SerializeField] private AudioClip _runSfx;
        [Range(0f, 1f), SerializeField] private float _sfxVolume = 1f;
        public void OnInteract()
        {
            var runMinigame = _changeScene.GetComponent<RunMinigame>();
            runMinigame.minigameSceneName = _sceneToLoad;
            runMinigame.runSfx = _runSfx;
            runMinigame.sfxVolume = _sfxVolume;
            _changeScene.SetActive(true);
        }
    }
}