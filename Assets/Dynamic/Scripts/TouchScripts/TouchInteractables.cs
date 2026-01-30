using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Interactables
{
    public class TouchInteractables : MonoBehaviour
    {
        void Update()
        {
            if(UI.UIState.IsUiOpen)
                return;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                int fingerId = Touchscreen.current.primaryTouch.touchId.ReadValue();
                Debug.Log("fingerId = " + fingerId);

                if (EventSystem.current.IsPointerOverGameObject(fingerId))
                {
                    return;
                }

                Debug.Log("Touch detected on screen.");
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                HandleTouch(touchPosition);
            }

    #if UNITY_EDITOR  

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                HandleTouch(mousePosition);
            }
    #endif
        }

        private void HandleTouch(Vector2 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.OnInteract();
                }
            }
        }
    }
}