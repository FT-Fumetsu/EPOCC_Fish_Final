using System.Collections;
using UnityEngine;

namespace FX
{
    public abstract class ParticleLauncher : FXLauncher<ParticleSystem>
    {
        [SerializeField] private bool _IsUnparent;
        
        protected override void PlayFX()
        {
            if (_IsUnparent)
            {
                transform.parent = null;
                transform.localScale = Vector3.one;
            }
            
            _fx.Play();

            IEnumerator DestroyCoroutine()
            {
                while (_fx.IsAlive())
                {
                    yield return null;
                }

                Destroy(gameObject);
            }

            if (_IsUnparent)
                StartCoroutine(DestroyCoroutine());
        }
    }
}