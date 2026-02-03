using Lure.Data;
using System.Collections.Generic;
using UnityEngine;
using Singletons;

namespace Manager.Lure
{
    public class LureManager : PersistentMonoSingleton<LureManager>
    {
        private Dictionary<LureData, int> _lureCounts = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Debug.Log("[LureManager] Initialized");
        }

        public void AddLure(LureData lure, int amount = 1)
        {
            if (lure == null) return;

            if (!_lureCounts.ContainsKey(lure))
                _lureCounts[lure] = 0;

            _lureCounts[lure] += amount;
            Debug.Log($"Ajout� {amount}x {lure.LureName}. Total : {_lureCounts[lure]}");
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
            Debug.Log($"Utilisé 1x {lure.LureName}. Il en reste : {_lureCounts[lure]}");

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
        

        public void Test()
        {
            Debug.Log(HasAnyLure() ? "Has any lure !" : "Has no lures !");
        }

        public override void Uninitialize()
        {
            _lureCounts.Clear();
            Debug.Log("[LureManager] Uninitialized");
        }
    }
}
