using System.Collections;
using Fish.Spawner.Data;
using Lure.Data;
using UnityEngine;

namespace Fishs.Spawner
{
    public class FishMonoSpawner : MonoBehaviour
    {
        [Header("Table par défaut (sans appât)")]
        [SerializeField] private FishSpawnData[] _defaultSpawnTable;

        [Header("GameObjects de référence pour les positions de spawn")]
        [SerializeField] private GameObject _minSpawnPosition;
        [SerializeField] private GameObject _maxSpawnPosition;
        
        [Header("Appâts disponibles")]
        [SerializeField] private LureData[] _lures;

        [Header("Appât actif")]
        [SerializeField] private int _activeLureIndex = -1;
        
        [Header("Délai entre spawns")]
        [SerializeField] private float _minDelay;
        [SerializeField] private float _maxDelay;

        public void StartSpawning()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));

                FishSpawnData[] table = GetCurrentSpawnTable();
                FishMovement fish = GetWeightedRandomFish(table);

                if (fish == null)
                {
                    continue;
                }
                
                var randomPosition = Vector3.Lerp(_minSpawnPosition.transform.position, _maxSpawnPosition.transform.position, Random.Range(0f, 1f));
                
                // Crée une rotation à partir de celle du spawner + 90° sur l'axe X
                var spawnRotation = transform.rotation * Quaternion.Euler(90f, 0f, 0f);
                var spawned = Instantiate(fish, randomPosition, spawnRotation, transform);
            }
        }
        
        FishSpawnData[] GetCurrentSpawnTable()
        {
            if (_defaultSpawnTable == null) _defaultSpawnTable = new FishSpawnData[0];

            if (_lures == null || _lures.Length == 0)
                return _defaultSpawnTable;
            
            if (_activeLureIndex < 0 || _activeLureIndex >= _lures.Length)
                return _defaultSpawnTable;

            return _lures[_activeLureIndex].SpawnTable;
        }

        FishMovement GetWeightedRandomFish(FishSpawnData[] table)
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
                    return fish.fishPrefab.GetComponent<FishMovement>();
            }

            return table[0].fishPrefab.GetComponent<FishMovement>();
        }
        
        public void SetActiveLure(LureData lure)
        {
            if (_lures == null || _lures.Length == 0)
            {
                _activeLureIndex = -1;
                return;
            }
        
            if (lure == null)
            {
                _activeLureIndex = -1;
                return;
            }
        
            for (int i = 0; i < _lures.Length; i++)
            {
                if (_lures[i] == lure)
                {
                    _activeLureIndex = i;
                    return;
                }
            }
        
            for (int i = 0; i < _lures.Length; i++)
            {
                if (_lures[i] != null && lure != null && _lures[i].name == lure.name)
                {
                    _activeLureIndex = i;
                    return;
                }
            }
        
            _activeLureIndex = -1;
        }
    }
}