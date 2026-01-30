using Fish.Spawner.Data;
using Lure.Data;
using System.Collections;
using UnityEngine;

namespace Fish.Spawner
{
    public class FishSpawner : MonoBehaviour
    {
        [Header("Table par défaut (sans appât)")]
        public FishSpawnData[] defaultSpawnTable;

        [Header("Appâts disponibles")]
        public LureData[] lures;

        [Header("Appât actif")]
        public int activeLureIndex = -1;

        [Header("Délai entre spawns")]
        public float minDelay = 5f;
        public float maxDelay = 15f;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

                FishSpawnData[] table = GetCurrentSpawnTable();
                GameObject fish = GetWeightedRandomFish(table);

                float rot = Random.Range(0f, 360f);
                Instantiate(fish, transform.position, Quaternion.Euler(0, 0, rot));
            }
        }

        FishSpawnData[] GetCurrentSpawnTable()
        {
            if (activeLureIndex < 0 || activeLureIndex >= lures.Length)
                return defaultSpawnTable;

            return lures[activeLureIndex].SpawnTable;
        }

        GameObject GetWeightedRandomFish(FishSpawnData[] table)
        {
            float total = 0f;
            foreach (var fish in table)
                total += fish.spawnChance;

            float rand = Random.Range(0f, total);
            float current = 0f;

            foreach (var fish in table)
            {
                current += fish.spawnChance;
                if (rand <= current)
                    return fish.fishPrefab;
            }

            return table[0].fishPrefab;
        }
    }
}