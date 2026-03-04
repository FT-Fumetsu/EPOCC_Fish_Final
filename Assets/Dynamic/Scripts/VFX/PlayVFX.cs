using System.Collections;
using UnityEngine;

namespace VFX
{
    public class PlayVFX : MonoBehaviour
    {
        [Tooltip("Si vrai, le GameObject contenant ce script sera détruit automatiquement après la fin des particle systems")] 
        [SerializeField] private bool _autoDestroy = true;
        [Tooltip("Délai de secours en secondes si la durée des particle systems ne peut pas être déterminée")]
        [SerializeField] private float _destroyFallback = 3f;

        void Start()
        {
            Play();
        }
        public void Play()
        {
            var systems = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
            if (systems == null || systems.Length == 0)
            {
                return;
            }

            float maxDuration = 0f;
            foreach (var ps in systems)
            {
                ps.gameObject.SetActive(true);

                var main = ps.main;
                
                main.useUnscaledTime = true;

                ps.Play(true);

                float duration = main.duration;
                try
                {
                    duration += main.startLifetime.constant;
                }
                catch
                {
                    // si pas accessible (ex: curve), on ignore
                }

                if (duration > maxDuration) maxDuration = duration;
            }

            if (_autoDestroy)
            {
                StartCoroutine(AutoDestroyAfterRealtime(maxDuration > 0f ? maxDuration : _destroyFallback));
            }
        }

        private IEnumerator AutoDestroyAfterRealtime(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            Destroy(gameObject);
        }
    }
}