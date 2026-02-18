using Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoSingleton<DragController>
{
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    private InputAction _positionAction;
    private InputAction _pressAction;
    private bool _prevPressed = false;

    private void OnEnable()
    {
        _positionAction = new InputAction("PointerPosition", binding: "<Pointer>/position");
        _pressAction = new InputAction("PointerPress", binding: "<Pointer>/press");

        _positionAction.Enable();
        _pressAction.Enable();
    }

    private void OnDisable()
    {
        if (_positionAction != null)
        {
            _positionAction.Disable();
            _positionAction.Dispose();
            _positionAction = null;
        }

        if (_pressAction == null) 
            return;
        
        _pressAction.Disable();
        _pressAction.Dispose();
        _pressAction = null;
    }

    private void Update()
    {
        Debug.Log(Pointer.current.position.value);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        if (_positionAction == null || _pressAction == null)
            return;

        bool isPressed = _pressAction.ReadValue<float>() > 0.5f;
        _screenPosition = _positionAction.ReadValue<Vector2>();

        if (_isDragActive && !isPressed && _prevPressed)
        {
            Drop();
            _prevPressed = isPressed;
            return;
        }

        if (!isPressed)
        {
            _prevPressed = isPressed;
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);

        if (_isDragActive)
        {
            if (_lastDragged != null)
            {
                Plane plane = new Plane(Vector3.up, new Vector3(0f, _lastDragged.transform.position.y, 0f));
                if (plane.Raycast(ray, out float enter))
                {
                    _worldPosition = ray.GetPoint(enter);
                }

                Drag();
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Draggable draggable = hit.transform.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }

        _prevPressed = isPressed;
    }

    private void InitDrag()
    {
        _isDragActive = true;
    }

    private void Drag()
    {
        if (_lastDragged == null)
            return;

        _lastDragged.transform.position = new Vector3(_worldPosition.x, _lastDragged.transform.position.y,
            _lastDragged.transform.position.z);
    }

    private void Drop()
    {
        _isDragActive = false;
    }
}
