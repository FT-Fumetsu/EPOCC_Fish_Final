using UnityEngine;

namespace Debugging
{
    public class OverlapDebugAtSpawn : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private LayerMask layerMask = ~0;
        [SerializeField] private bool includeTriggers = true;

        private Collider[] _lastFound = new Collider[0];

        private void Start()
        {
            CheckOverlap();
        }

        private void Update()
        {
            
        }

        public void CheckOverlap()
        {
            QueryTriggerInteraction qti = includeTriggers ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore;
            _lastFound = Physics.OverlapSphere(transform.position, radius, layerMask, qti);

            Debug.Log($"[OverlapDebug] Position: {transform.position}, radius: {radius}, found: {_lastFound.Length}");

            for (int i = 0; i < _lastFound.Length; i++)
            {
                var c = _lastFound[i];
                if (c == null) continue;
                var rb = c.attachedRigidbody;
                Debug.Log($"[OverlapDebug] Collider {i}: name={c.name}, type={c.GetType().Name}, isTrigger={c.isTrigger}, layer={LayerMask.LayerToName(c.gameObject.layer)}, attachedRigidbody={(rb != null ? rb.name : "null")}", c);
            }

            // visualisation simple : ligne vers le bas et sphere
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.5f, Color.yellow, 5f);
            Debug.DrawRay(transform.position, Vector3.up * 0.1f, Color.green, 5f);
        }

        public void DisableColliders()
        {
            foreach (var c in _lastFound)
            {
                if (c != null)
                {
                    Debug.Log($"[OverlapDebug] Désactivation temporaire du collider: {c.name} (isTrigger={c.isTrigger})", c);
                    c.enabled = false;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}