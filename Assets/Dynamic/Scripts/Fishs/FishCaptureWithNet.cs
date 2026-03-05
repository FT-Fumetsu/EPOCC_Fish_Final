using System;
using Manager.Fishs;
using UnityEngine;

namespace Fish.Interactable
{
    
    public class FishCaptureWithNet : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;
        [SerializeField] private NetCounter _netCounter;

        private void Start()
        {
            _netCounter = (NetCounter)FindFirstObjectByType(typeof(NetCounter));
        }

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
            _netCounter.FishCount += 1;
            _netCounter.FishCounterText.text = $"Poissons : {_netCounter.FishCount}";
            Destroy(gameObject);
        }
    }
}