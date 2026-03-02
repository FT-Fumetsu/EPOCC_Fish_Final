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

        private LureData _currentLureData;
        public void SetupLureUI(LureData data, SetAllLuresUI luresLayout )
        {
            _currentLureData = data;
            _luresLayout = luresLayout;
            _lureIcon.sprite = data.LureIcon;
            _lureCost = data.LurePrice;
            _lurePrice.text = (data.LurePrice.ToString());
        }

        public void OpenTradePanel()
        {
            
            if(_luresLayout != null)
            {
                _luresLayout.LureTradeUi.SetActive(true);
                _luresLayout.SetTradePanel(_currentLureData);
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