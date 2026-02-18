using UnityEngine;

namespace Interactables.Trader
{
    [RequireComponent(typeof(Collider))]
    public class TraderInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _tradeUi;
        [SerializeField] private GameObject _lureTradeUi;

        [SerializeField] private GameObject _mobileControls;
        public void OnInteract()
        {
            Debug.Log("Interact !");
            if (_tradeUi != null)
            {
                Debug.LogWarning("Trade UI !");
                _tradeUi.SetActive(true);
                _lureTradeUi.SetActive(false);
                if (_mobileControls != null)
                {
                    _mobileControls.SetActive(false);
                }
            }
        }
    }
}
