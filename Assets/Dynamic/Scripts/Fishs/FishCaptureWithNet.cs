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
            Capture();
        }

        private void OnCollisionEnter(Collision collision)
        {
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