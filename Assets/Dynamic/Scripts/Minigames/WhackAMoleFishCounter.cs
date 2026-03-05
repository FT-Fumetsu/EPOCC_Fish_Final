using TMPro;
using UnityEngine;

public class WhackAMoleFishCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fishCounterText;
    [SerializeField] private int _fishCount = 0;
    
    public TextMeshProUGUI FishCounterText => _fishCounterText;

    public int FishCount
    {
        get => _fishCount;
        set => _fishCount = value;
    }
    
    private void Start()
    {
        if (_fishCounterText == null)
        {
            Debug.LogError("Fish Counter Text is not assigned in the inspector.");
            return;
        }

        _fishCounterText.text = "Poissons : 0";
    }
}
