using Manager.Fishs;
using UnityEngine;

namespace Fish.Interactable
{
    
    public class FishCaptureWithNet : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("FishInteractable: Trigger entered by " + other.gameObject.name);
            Capture();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("FishInteractable: Collision entered by " + collision.gameObject.name);
            Capture();
        }

        private void Capture()
        {
            FishsManager.Instance.AddFishs(_fishCount);
            FishingCodex.Instance?.RecordFishCatch(_fishData);
            Destroy(gameObject);
        }
    }
}