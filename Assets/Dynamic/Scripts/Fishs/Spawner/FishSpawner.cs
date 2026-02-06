using Fish.Spawner.Data;
using Lure.Data;
using System.Collections;
using Fish.StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fish.Spawner
{
    public class FishSpawner : MonoBehaviour
    {
        [Header("Minigame Bool")]
        [SerializeField] private bool _tapeTaupe;
        
        [Header("Table par défaut (sans appât)")]
        [SerializeField] private FishSpawnData[] defaultSpawnTable;

        [Header("Appâts disponibles")]
        [SerializeField] private LureData[] lures;

        [Header("Appât actif")]
        [SerializeField] private int activeLureIndex = -1;

        [Header("Délai entre spawns")]
        [SerializeField] private float minDelay = 5f;
        [SerializeField] private float maxDelay = 15f;
        
        [Header("Angle de spawn (deg)")]
        [SerializeField] private float minSpawnAngle = -45f;
        [SerializeField] private float maxSpawnAngle = 45f;
        
        [Header("Gizmos")]
        [SerializeField] private float gizmoRadius = 20f;
        [SerializeField] private int gizmoSegments = 32;

        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

                FishSpawnData[] table = GetCurrentSpawnTable();
                FishStateMachine fish = GetWeightedRandomFish(table);

                if (fish == null)
                {
                    Debug.LogWarning("FishSpawner: No valid fish prefab found to spawn.");
                    continue;
                }
                
                float randomOffset = Random.Range(minSpawnAngle, maxSpawnAngle);
                var finalZ = transform.rotation.eulerAngles.z + randomOffset;
                
                Instantiate(fish, transform.position, Quaternion.Euler(0f, 0f, finalZ));
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

        FishStateMachine GetWeightedRandomFish(FishSpawnData[] table)
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
                    return fish.fishPrefab.GetComponent<FishStateMachine>();
            }

            return table[0].fishPrefab.GetComponent<FishStateMachine>();
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
        
#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (!Application.isPlaying)
            {
                Gizmos.color = new Color(0f, 1f, 1f, 0.6f);
            }
            else
            {
                Gizmos.color = Color.cyan;
            }

            Vector3 center = transform.position;
            Quaternion baseRot = transform.rotation;
            float startAngle = /* transform.rotation.eulerAngles.z + */ minSpawnAngle;
            float endAngle = /* transform.rotation.eulerAngles.z + */ maxSpawnAngle;
            int segments = Mathf.Max(3, gizmoSegments);
            float radius = Mathf.Max(0.1f, gizmoRadius);

            Vector3 prevPoint = center + (baseRot * Quaternion.Euler(0, 0, startAngle) * Vector3.right) * radius;
            for (int i = 1; i <= segments; i++)
            {
                float t = (float)i / segments;
                float angle = Mathf.Lerp(startAngle, endAngle, t);
                Vector3 nextPoint = center + (baseRot * Quaternion.Euler(0, 0, angle) * Vector3.right) * radius;
                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }

            Vector3 dirStart = baseRot * Quaternion.Euler(0, 0, startAngle) * Vector3.right;
            Vector3 dirEnd = baseRot * Quaternion.Euler(0, 0, endAngle) * Vector3.right;
            Gizmos.DrawLine(center, center + dirStart * radius);
            Gizmos.DrawLine(center, center + dirEnd * radius);
        }
#endif
    }
}