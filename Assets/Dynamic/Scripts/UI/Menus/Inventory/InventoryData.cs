using System;
using Lure.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryData : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    
    [Header("Variables")]
    [Tooltip("Si True, c'est un appât, sinon c'est un poisson")]
    [SerializeField] private bool _isLure;
    [SerializeField] private LureData _currentLureData;
    [SerializeField] private FishData _currentFish;

    private void OnEnable()
    {
        if (_isLure)
        {
            if (_currentLureData == null)
                return;
            _icon.sprite = _currentLureData.LureIcon;
            _countText.text = Manager.Lure.LureManager.Instance?.GetLureCount(_currentLureData).ToString();
            Debug.LogWarning("Lure !");
        }
        else
        {
            if (_currentFish == null)
                return;
            _icon.sprite = _currentFish.Icon;
            _countText.text = FishingCodex.Instance?.GetCountForFish(_currentFish).ToString();
            Debug.LogWarning("Fish !");
        }
    }
}
