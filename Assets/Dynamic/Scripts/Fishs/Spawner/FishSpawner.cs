using Fish.Spawner.Data;
using Lure.Data;
using System.Collections;
using UnityEngine;

namespace Fish.Spawner
{
    public class FishSpawner : MonoBehaviour
    {
        [Header("Table par défaut (sans appât)")]
        [SerializeField] private FishSpawnData[] defaultSpawnTable;

        [Header("Appâts disponibles")]
        [SerializeField] private LureData[] lures;

        [Header("Appât actif")]
        [SerializeField] private int activeLureIndex = -1;

        [Header("Délai entre spawns")]
        [SerializeField] private float _minDelay = 5f;
        [SerializeField] private float _maxDelay = 15f;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));

                FishSpawnData[] table = GetCurrentSpawnTable();
                GameObject fish = GetWeightedRandomFish(table);

                if (fish == null)
                {
                    Debug.LogWarning("FishSpawner: No valid fish prefab found to spawn.");
                    continue;
                }
                
                float rot = Random.Range(0f, 360f);
                Instantiate(fish, transform.position, Quaternion.Euler(0, 0, rot));
            }
        }

        FishSpawnData[] GetCurrentSpawnTable()
        {
            if (defaultSpawnTable == null) defaultSpawnTable = new FishSpawnData[0];

            if (lures == null || lures.Length == 0)
                return defaultSpawnTable;
            
            if (activeLureIndex < 0 || activeLureIndex >= lures.Length)
                return defaultSpawnTable;

            return lures[activeLureIndex].SpawnTable;
        }

        GameObject GetWeightedRandomFish(FishSpawnData[] table)
        {
            float total = 0f;
            foreach (var fish in table)
                total += fish.spawnWeight;

            float rand = Random.Range(0f, total);
            float current = 0f;

            foreach (var fish in table)
            {
                current += fish.spawnWeight;
                if (rand <= current)
                    return fish.fishPrefab;
            }

            return table[0].fishPrefab;
        }
        
        public void SetActiveLure(LureData lure)
        {
            if (lures == null || lures.Length == 0)
            {
                Debug.LogWarning("FishSpawner: aucun appât configuré.");
                activeLureIndex = -1;
                return;
            }
        
            if (lure == null)
            {
                activeLureIndex = -1;
                return;
            }
        
            for (int i = 0; i < lures.Length; i++)
            {
                if (lures[i] == lure)
                {
                    activeLureIndex = i;
                    return;
                }
            }
        
            for (int i = 0; i < lures.Length; i++)
            {
                if (lures[i] != null && lure != null && lures[i].name == lure.name)
                {
                    activeLureIndex = i;
                    return;
                }
            }
        
            Debug.LogWarning($"FishSpawner: appât non trouvé dans le tableau `lures` : {lure.name}");
            activeLureIndex = -1;
        }
        
    }
}