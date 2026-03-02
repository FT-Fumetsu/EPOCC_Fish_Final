using Lure.Data;
using System.Collections.Generic;
using UnityEngine;
using Singletons;
using Unity.VisualScripting;

namespace Manager.Lure
{
    public class LureManager : PersistentMonoSingleton<LureManager>
    {
        private Dictionary<LureData, int> _lureCounts = new();
        
        [SerializeField] private LureData[] _lureDatabase;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void AddLure(LureData lure, int amount = 1)
        {
            if (lure == null) return;

            if (!_lureCounts.ContainsKey(lure))
                _lureCounts[lure] = 0;

            _lureCounts[lure] += amount;
        }

        public bool HasLure(LureData lure)
        {
            return lure != null &&
                   _lureCounts.TryGetValue(lure, out int count) &&
                   count > 0;
        }

        public bool HasAnyLure()
        {
            return _lureCounts.Count > 0;
        }

        public bool BoolUseLure(LureData lure)
        {
            if (!HasLure(lure))
                return false;

            _lureCounts[lure]--;

            return true;
        }

        public int GetLureCount(LureData lure)
        {
            if (lure == null || !_lureCounts.ContainsKey(lure))
                return 0;

            return _lureCounts[lure];
        }
        
        public IEnumerable<LureData> GetAllLures()
        {
            return new List<LureData>(_lureCounts.Keys);
        }

        public Dictionary<int, int> Serialize()
        {
            Dictionary<int, int> serializedData = new();
            
            foreach (KeyValuePair<LureData, int> countByLureDataItem in _lureCounts)
            {
                int lureId = System.Array.IndexOf(_lureDatabase, countByLureDataItem.Key);
                if (lureId >= 0)
                {
                    serializedData[lureId] = countByLureDataItem.Value;
                }
            }
            
            return serializedData;
        }
        
        public void Deserialize(Dictionary<int, int> serializedData)
        {
            _lureCounts.Clear();
            
            foreach (KeyValuePair<int, int> item in serializedData)
            {
                if (item.Key >= 0 && item.Key < _lureDatabase.Length)
                {
                    LureData lure = _lureDatabase[item.Key];
                    _lureCounts[lure] = item.Value;
                }
            }
        }

        public override void Uninitialize()
        {
            _lureCounts.Clear();
        }
    }
}
