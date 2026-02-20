using Lure.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData")]
public class FishData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    
    [Header("Codex Infos")]
    [SerializeField] private Sprite _codexIcon;
    [SerializeField] private string _fishName;
    [SerializeField] private string _fishSize;
    [SerializeField] private string _fishFood;
    
    [TextArea]
    [SerializeField] private string _description;
    
    [Header("Lures for Codex")]
    [SerializeField] private LureData[] _luresToUnlock;
    
    public Sprite Icon => _icon;
    public Sprite CodexIcon => _codexIcon;
    public string FishName => _fishName;
    public string FishSize => _fishSize;
    public string FishFood => _fishFood;
    public string Description => _description;
    public LureData[] LuresToUnlock => _luresToUnlock;
}

