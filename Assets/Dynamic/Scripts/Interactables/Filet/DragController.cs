using Singletons;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoSingleton<DragController>
{
    [SerializeField] private GameObject _minPosition;
    [SerializeField] private GameObject _maxPosition;

    private bool _hasClickedOnce;
    private void Update()
    {
        if (Pointer.current == null)
        {
            if (_minPosition)
                transform.position = _minPosition.transform.position;
            return;
        }

        if (!_hasClickedOnce)
        {
            var pressControl = Pointer.current.press;
            bool isPressed = pressControl != null && pressControl.isPressed;

            if (!isPressed)
            {
                if (_minPosition)
                    transform.position = _minPosition.transform.position;
                return;
            }

            _hasClickedOnce = true;
        }

        Vector2 pointerPos = Pointer.current.position.ReadValue();
        float delta = pointerPos.x / Screen.width;

        delta = Mathf.Clamp01(delta);
        if (_minPosition && _maxPosition)
            transform.position = Vector3.Lerp(_minPosition.transform.position, _maxPosition.transform.position, delta);
    }
}
