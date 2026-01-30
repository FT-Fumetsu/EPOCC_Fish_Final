using UnityEngine;

namespace Fish.Spawner.Data
{
    [System.Serializable]
    public class FishSpawnData
    {
        public GameObject fishPrefab;

        [Tooltip("Chance to spawn this fish (0 to 1)")]
        [Range(0, 1)] public float spawnChance;

        [HideInInspector]
        public float currentChance;
    }
}