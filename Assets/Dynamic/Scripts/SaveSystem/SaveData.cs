using System.Collections.Generic;
using UnityEngine;

namespace Save.Data
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;

        public int totalFishCount;
        
        public Dictionary<int, int> countByLure;
        public Dictionary<int, int> countByFish;
    }
}