using UnityEngine;

namespace Manager.Fishs
{
    public class FishsManager : MonoBehaviour
    {
        public static FishsManager Instance;

        [SerializeField] private int _fishsCount = 0;

        public int FishsCount => _fishsCount;

        private void Awake()
        {
            Instance = this;
        }

        public void AddFishs(int fish)
        {
            _fishsCount = _fishsCount + fish;
            Debug.Log("Fishs Count = " + _fishsCount);
        }

        public void RemoveFishs(int fish)
        {
            if(_fishsCount > 0)
            {
                _fishsCount = _fishsCount - fish;
                Debug.Log("Fishs Count = " + _fishsCount);
            }
        }
    }
}