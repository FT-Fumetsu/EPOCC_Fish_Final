using UnityEngine;

namespace Interactables
{
    // Simple interface contract. Implementations should call the pause check
    // or derive from InteractableBase which already performs it.
    public interface IInteractable
    {
        void OnInteract();
    }
}