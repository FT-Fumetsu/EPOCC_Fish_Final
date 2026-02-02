using Manager.Fishs;
using Manager.Lure;
using Lure.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TraderPanel
{
    public class SetAllLuresUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _luresLayoutGroup;
        [SerializeField] private GameObject _lureTradeUi;

        [SerializeField] private GameObject _lureUIPrefab;
        [SerializeField] private LureData[] _lureDatas;

        [Header("TradePanel")]
        [SerializeField] private Image _tradeIcon;
        [SerializeField] private TMP_Text _tradePriceText;

        private readonly List<GameObject> _spawnedItems = new();

        private int _currentFishPrice;
        private LureData _currentData;

        public int CurrentFishPrice
        {
            get => _currentFishPrice;
            set => _currentFishPrice = value;
        }

        public GameObject LureTradeUi => _lureTradeUi;

        private void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            ClearItems();

            if(_luresLayoutGroup == null || _lureUIPrefab == null || _lureDatas.Length == 0)
            {
                Debug.LogWarning("SetAllLuresUI: Missing references or no lure data available.");
                return;
            }

            foreach (var lureData in _lureDatas)
            {
                var prefab = Instantiate(_lureUIPrefab, _luresLayoutGroup);
                prefab.transform.SetParent(_luresLayoutGroup, false);
                var item = prefab.GetComponent<Lure.UI.SetupLurePrefabsUi>();
                if(item != null)
                {
                    item.SetupLureUI(lureData, this);
                    item.CheckTradeAvailable(lureData.LurePrice, lureData);
                }
                _spawnedItems.Add(prefab);
            }
        }

        public void ClearItems()
        {
            for (int i = 0; i < _spawnedItems.Count; i++)
            {
                if(_spawnedItems[i] != null)
                {
                    Destroy(_spawnedItems[i]);
                }
            }
            _spawnedItems.Clear();
        }

        public void SetTradePanel(LureData data)
        {
            _currentData = data;
            _tradeIcon.sprite = data.LureIcon;
            _tradePriceText.text = data.LurePrice.ToString();
            _currentFishPrice = data.LurePrice;
            Debug.Log("Prix = " + data.LurePrice);
        }

        public void AcceptTrade()
        {
            if(_currentFishPrice > FishsManager.Instance.FishsCount)
            {
                Debug.LogWarning("SetAllLuresUI: Not enough fish to complete the trade.");
                return;
            }

            FishsManager.Instance.RemoveFishs(_currentFishPrice);
            LureManager.Instance.AddLure(_currentData, 1);
        }
    }
}