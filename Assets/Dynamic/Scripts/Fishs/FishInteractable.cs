using Manager.Fishs;
using UnityEngine;

namespace Interactables.Fishs
{
    [RequireComponent(typeof(Collider))]
    public class FishInteractable : MonoBehaviour, IInteractable
    {
        public void OnInteract()
        {
            FishsManager.Instance.AddFishs(1);
            //Destroy(gameObject);
        }
    }
}