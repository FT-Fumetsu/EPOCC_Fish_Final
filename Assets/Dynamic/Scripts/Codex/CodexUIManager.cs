using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Serialization;

public class CodexUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _fishIcon;
    [SerializeField] private Sprite _hiddenFishIcon;
    [SerializeField] private TextMeshProUGUI _fishNameText;
    [SerializeField] private TextMeshProUGUI _fishSizeText;
    [SerializeField] private TextMeshProUGUI _fishFoodText;
    [SerializeField] private TextMeshProUGUI _fishDescText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    [Header("Fish List")]
    [SerializeField] private List<FishData> _allFishData = new();

    private int currentPageIndex = 0;

    private void Start()
    {
        _prevButton.onClick.AddListener(OnPrevPage);
        _nextButton.onClick.AddListener(OnNextPage);

        UpdatePage();
    }

    public void UpdatePage()
    {
        _fishNameText.text = "";
        _fishSizeText.text = "";
        _fishFoodText.text = "";
        _fishDescText.text = "";
        _fishIcon.sprite = _hiddenFishIcon;
        
        if (_allFishData.Count == 0)
            return;

        FishData currentFish = _allFishData[currentPageIndex];

        int count = FishingCodex.Instance.GetCountForFish(currentFish);

        if (FishingCodex.Instance.IsNameUnlocked(currentFish))
        {
            _fishNameText.text = currentFish.FishName;
        }
        else
        {
            _fishNameText.text = "Pêchez plus de cette espèce pour révéler son nom !";
            return;
        }

        if (FishingCodex.Instance.IsIconUnlocked(currentFish))
        {
            _fishIcon.sprite = currentFish.Icon;
        }
        else
        {
            _fishIcon.sprite = _hiddenFishIcon;
            _fishSizeText.text = "Pêchez plus de cette espèce pour révéler sa taille !";
            return;
        }
        
        if (FishingCodex.Instance.IsSizeUnlocked(currentFish))
        {
            _fishSizeText.text = currentFish.FishSize;
        }
        else
        {
            _fishSizeText.text = "Pêchez plus de cette espèce pour révéler sa taille !";
            return;
        }

        if (FishingCodex.Instance.IsFoodUnlocked(currentFish))
        {
            _fishFoodText.text = currentFish.FishFood;
        }
        else
        {
            _fishFoodText.text = "Pêchez plus de cette espèce pour révéler sa nourriture préférée !";
            return;
        }
        
        if (FishingCodex.Instance.IsDescriptionUnlocked(currentFish))
        {
            _fishDescText.text = currentFish.Description;
        }
        else
        {
            _fishDescText.text = "Pêchez plus de cette espèce pour révéler sa description !";
        }
    }


    public void OnPrevPage()
    {
        if (currentPageIndex > 0)
            currentPageIndex--;

        UpdatePage();
    }

    public void OnNextPage()
    {
        if (currentPageIndex < _allFishData.Count - 1)
            currentPageIndex++;

        UpdatePage();
    }
}