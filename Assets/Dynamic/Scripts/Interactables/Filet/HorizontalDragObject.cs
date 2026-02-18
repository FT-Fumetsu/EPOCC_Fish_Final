using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactables
{
    [RequireComponent(typeof(Collider))]
    public class HorizontalDragObject : MonoBehaviour, IInteractable
    {
        private Camera _mainCamera;
        private bool _isDragging = false;

        private Plane _dragPlane;
        private float _fixedY;
        private float _fixedZ;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _fixedY = transform.position.y;
            _fixedZ = transform.position.z;

            _dragPlane = new Plane(Vector3.up, new Vector3(0, _fixedY, 0));
        }

        public void OnInteract()
        {
            _isDragging = true;
        }

        private void Update()
        {
            if (!_isDragging)
                return;

            if (Touchscreen.current == null)
                return;

            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                DragObject(touchPosition);
            }
            else
            {
                _isDragging = false;
            }
        }

#if UNITY_EDITOR
        private void OnMouseDrag()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            DragObject(mousePosition);
        }
#endif

        private void DragObject(Vector2 screenPosition)
        {
            Ray ray = _mainCamera.ScreenPointToRay(screenPosition);

            if (_dragPlane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);

                transform.position = new Vector3(
                    worldPoint.x,  // Seul X change
                    _fixedY,
                    _fixedZ
                );
            }
        }
    }
}