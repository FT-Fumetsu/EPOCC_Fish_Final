using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData")]
public class FishData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _codexIcon;
    [SerializeField] private string _fishName;
    [SerializeField] private string _fishSize;
    [SerializeField] private string _fishFood;
    
    [TextArea]
    [SerializeField] private string _description;
    
    public Sprite Icon => _icon;
    public Sprite CodexIcon => _codexIcon;
    public string FishName => _fishName;
    public string FishSize => _fishSize;
    public string FishFood => _fishFood;
    public string Description => _description;
    
}

