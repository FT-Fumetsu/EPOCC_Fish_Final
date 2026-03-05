using System;
using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;
        [SerializeField] private WhackAMoleFishCounter _whackCounter;

        private void Start()
        {
            _whackCounter = (WhackAMoleFishCounter)FindFirstObjectByType(typeof(WhackAMoleFishCounter));
        }

        public void OnInteract()
        {
            Capture();
        }

        private void Capture()
        {
            FishsManager.Instance.AddFishs(_fishCount);
            FishingCodex.Instance?.RecordFishCatch(_fishData);
            _whackCounter.FishCount += 1;
            _whackCounter.FishCounterText.text = $"Poissons : {_whackCounter.FishCount}";
            Destroy(gameObject);
        }
    }
}