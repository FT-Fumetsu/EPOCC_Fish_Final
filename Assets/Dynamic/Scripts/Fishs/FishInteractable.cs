using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        [SerializeField] private FishData _fishData;
        
        [Header("VFX")]
        [Tooltip("Prefab VFX à instancier quand le poisson est capturé. Peut contenir un script PlayVFX pour auto-play/destroy.")]
        [SerializeField] private GameObject _captureVfxPrefab;

        public void OnInteract()
        {
            Capture();
        }

        private void Capture()
        {
            if (_captureVfxPrefab)
            {
                var vfx = Instantiate(_captureVfxPrefab, transform.position, Quaternion.identity);
                var play = vfx.GetComponent<VFX.PlayVFX>();
                if (play)
                {
                    play.Play();
                }
                else
                {
                    var childPlay = vfx.GetComponentInChildren<VFX.PlayVFX>();
                    childPlay?.Play();
                }
            }

            FishsManager.Instance.AddFishs(_fishCount);
            FishingCodex.Instance?.RecordFishCatch(_fishData);
            Destroy(gameObject);
        }
    }
}