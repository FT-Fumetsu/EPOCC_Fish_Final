using UnityEngine;
using Singletons;

namespace Manager.Fishs
{
    public class FishsManager : PersistentMonoSingleton<FishsManager>
    {
        [SerializeField] private int _fishsCount = 0;

        public int FishsCount => _fishsCount;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Debug.Log("[FishsManager] Initialized");
        }

        public void AddFishs(int fish)
        {
            if (fish <= 0) return;

            _fishsCount += fish;
            Debug.Log($"Fishs Count = {_fishsCount}");
        }

        public void RemoveFishs(int fish)
        {
            if (fish <= 0) return;

            _fishsCount = Mathf.Max(0, _fishsCount - fish);
            Debug.Log($"Fishs Count = {_fishsCount}");
        }

        public override void Uninitialize()
        {
            _fishsCount = 0;
            Debug.Log("[FishsManager] Uninitialized");
        }
    }
}
