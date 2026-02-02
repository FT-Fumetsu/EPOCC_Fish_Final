using UnityEngine;

namespace Fish.Spawner.Data
{
    [System.Serializable]
    public class FishSpawnData
    {
        public GameObject fishPrefab;

        [Tooltip("Weight of the fish to spawn")]
        [Range(0, 1)] public float spawnWeight;

        [HideInInspector]
        public float currentChance;
    }
}