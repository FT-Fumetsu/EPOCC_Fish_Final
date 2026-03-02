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


            for (int i = 0; i < _lastFound.Length; i++)
            {
                var c = _lastFound[i];
                if (c == null) continue;
                var rb = c.attachedRigidbody;
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