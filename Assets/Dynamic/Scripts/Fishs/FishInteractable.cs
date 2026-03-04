using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;

        public void OnInteract()
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