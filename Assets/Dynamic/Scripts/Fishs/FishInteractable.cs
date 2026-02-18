using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Tooltip("La scène dans laquelle les poissons ne peuvent plus être touchés")] 
        private string _sceneName;  
        
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;

        // Prevent double capture via both interaction and collision
        private bool _isCaptured = false;

        public void OnInteract()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == _sceneName)
                return;

            Capture();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("FishInteractable: Trigger entered by " + other.gameObject.name);
            TryCaptureByDraggable(other.gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("FishInteractable: Collision entered by " + collision.gameObject.name);
            TryCaptureByDraggable(collision.gameObject);
        }

        private void TryCaptureByDraggable(GameObject other)
        {
            if (_isCaptured)
                return;

            if (other.GetComponent<Draggable>() == null) 
                return;
            
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == _sceneName)
            {
                Capture();
            }
        }

        private void Capture()
        {
            if (_isCaptured)
                return;

            _isCaptured = true;
            FishsManager.Instance.AddFishs(_fishCount);
            FishingCodex.Instance?.RecordFishCatch(_fishData);
            Destroy(gameObject);
        }
    }
}