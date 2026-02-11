using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CodexUIManager : MonoBehaviour
{
    [SerializeField] private Image _fishIcon;
    [SerializeField] private Image _hiddenFishIcon;
    [SerializeField] private Text _fishNameText;
    [SerializeField] private Text _fishDescText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    [Header("Fish List")]
    [SerializeField] private List<FishData> _allFishData = new List<FishData>();

    private int currentPageIndex = 0;

    private void Start()
    {
        _prevButton.onClick.AddListener(OnPrevPage);
        _nextButton.onClick.AddListener(OnNextPage);

        UpdatePage();
    }

    public void UpdatePage()
    {
        if (_allFishData.Count == 0) return;

        var currentFish = _allFishData[currentPageIndex];

        var count = FishingCodex.Instance.GetCountForFish(currentFish);

        _fishNameText.text = FishingCodex.Instance.IsNameUnlocked(currentFish) ? currentFish.fishName : "???";

        _fishIcon.sprite = FishingCodex.Instance.IsIconUnlocked(currentFish) ? currentFish.icon : _hiddenFishIcon.sprite;

        if (FishingCodex.Instance.IsDescriptionUnlocked(currentFish))
            _fishDescText.text = currentFish.description;
        else if (count > 0)
            _fishDescText.text = $"Pêché {count} fois — continue pour en apprendre plus !";
        else
            _fishDescText.text = "Poisson non découvert.";
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