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

        private SetAllLuresUI _luresLayout;
        public void SetupLureUI(LureData data, SetAllLuresUI luresLayout)
        {
            _luresLayout = luresLayout;
            _lureIcon.sprite = data.LureIcon;
            _lurePrice.text = ("Price = " + data.LurePrice);
        }

        public void OpenTradePanel()
        {
            if(_luresLayout != null)
            {
                _luresLayout.LureTradeUi.SetActive(true);
            }
        }
    }
}