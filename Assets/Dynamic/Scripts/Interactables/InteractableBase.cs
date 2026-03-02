using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Base helper for interactable objects. It centralizes the Pause check so
    /// every interactable doesn't need to repeat the same code.
    /// Derive from this and implement Interact() instead of OnInteract().
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public void OnInteract()
        {
            if (Manager.Pause.PauseManager.Instance?.IsPaused == true)
                return;

            Interact();
        }

        protected abstract void Interact();
    }
}

