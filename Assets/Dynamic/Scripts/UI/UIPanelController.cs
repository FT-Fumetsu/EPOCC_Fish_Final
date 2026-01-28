using UnityEngine;

namespace UI
{
    public class UIPanelController : MonoBehaviour
    {
        private void OnEnable() => UIState.SetUiOpen(true);
        private void OnDisable() => UIState.SetUiOpen(false);

        public void ClosePanel() => gameObject.SetActive(false);
    }
}