using UnityEngine;
using Singletons;

namespace Manager.Fishs
{
    public class FishsManager : PersistentMonoSingleton<FishsManager>
    {
        [SerializeField] private int _fishsCount = 0;

        public int FishsCount
        {
            get => _fishsCount;
            set => _fishsCount = value;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void AddFishs(int fish)
        {
            if (fish <= 0) return;

            _fishsCount += fish;
        }

        public void RemoveFishs(int fish)
        {
            if (fish <= 0) return;

            _fishsCount = Mathf.Max(0, _fishsCount - fish);
        }

        public override void Uninitialize()
        {
            _fishsCount = 0;
        }
    }
}
