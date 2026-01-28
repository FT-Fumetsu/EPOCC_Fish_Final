using UnityEngine;

namespace Interactables.Trader
{
    public class TraderInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _tradeUi;
        [SerializeField] private GameObject _lureTradeUi;
        public void OnInteract()
        {
            if (_tradeUi != null)
            {
                _tradeUi.SetActive(true);
                _lureTradeUi.SetActive(false);
            }
        }
    }
}
