using Manager.Fishs;
using Lure.Data;
using TMPro;
using UI.TraderPanel;
using UnityEngine;
using UnityEngine.UI;

namespace Lure.UI
{
    public class SetupLurePrefabsUi : MonoBehaviour
    {
        [SerializeField] private Image _lureIcon;
        [SerializeField] private TMP_Text _lurePrice;
        [SerializeField] private Button _tradeButton;

        private SetAllLuresUI _luresLayout;
        private int _lureCost;
        public void SetupLureUI(LureData data, SetAllLuresUI luresLayout )
        {
            _luresLayout = luresLayout;
            _lureIcon.sprite = data.LureIcon;
            _lureCost = data.LurePrice;
            _lurePrice.text = ("Price = " + data.LurePrice.ToString());
        }

        public void OpenTradePanel()
        {
            if(_luresLayout != null)
            {
                _luresLayout.LureTradeUi.SetActive(true);
                _luresLayout.SetTradePanel(_lureIcon.sprite, _lurePrice.text + " Poissongs", _lureCost);
            }
        }

        public void CheckTradeAvailable(int price, LureData lureData)
        {
            if (FishsManager.Instance.FishsCount < price)
            {
                _lurePrice.color = Color.red;
                _tradeButton.interactable = false;
            }
            else
            {
                _lurePrice.color = Color.black;
                _tradeButton.interactable = true;
            }
        }
    }
}