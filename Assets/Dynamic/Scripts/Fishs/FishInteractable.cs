using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField, Range(1, 100)] private int _fishCount;
        public void OnInteract()
        {
            FishsManager.Instance.AddFishs(_fishCount);
            Destroy(gameObject);
        }
    }
}