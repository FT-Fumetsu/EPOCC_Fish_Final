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
            Debug.Log($"Ajouté {amount}x {lure.LureName}. Total : {_lureCounts[lure]}");
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

        public bool UseLure(LureData lure)
        {
            if (!HasLure(lure))
                return false;

            _lureCounts[lure]--;
            Debug.Log($"Utilisé 1x {lure.LureName}. Il en reste : {_lureCounts[lure]}");

            return true;
        }

        public int GetLureCount(LureData lure)
        {
            return lure != null && _lureCounts.TryGetValue(lure, out int count)
                ? count
                : 0;
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
