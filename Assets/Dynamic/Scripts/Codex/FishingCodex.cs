using System.Collections.Generic;
using Singletons;
using UnityEngine;

public class FishingCodex : PersistentMonoSingleton<FishingCodex>
{
    [Header("Configuration")]
    [Tooltip("Nombre de poissons à pêcher pour révéler le nom")]
    [SerializeField] private int _fishCountForName = 1;

    [Tooltip("Nombre de poissons à pêcher pour révéler la description")]
    [SerializeField] private int _fishCountForDescription = 5;

    [Tooltip("Nombre de poissons à pêcher pour révéler l'icône")]
    [SerializeField] private int _fishCountForIcon = 3;

    [Tooltip("Nombre de poissons à pêcher pour révéler la taille")]
    [SerializeField] private int _fishCountForSize = 7;

    private Dictionary<FishData, int> _fishCounts = new Dictionary<FishData, int>();

    public static FishingCodex Instance { get; private set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Debug.Log("[FishingCodex] Initialized");
    }
    
    public void RecordFishCatch(FishData fish)
    {
        if (_fishCounts.ContainsKey(fish))
            _fishCounts[fish]++;
        else
            _fishCounts.Add(fish, 1);
    }

    public int GetCountForFish(FishData fish)
    {
        if (_fishCounts.ContainsKey(fish))
            return _fishCounts[fish];

        return 0;
    }

    public bool IsNameUnlocked(FishData fish)
    {
        return GetCountForFish(fish) >= _fishCountForName;
    }

    public bool IsDescriptionUnlocked(FishData fish)
    {
        return GetCountForFish(fish) >= _fishCountForDescription;
    }

    public bool IsIconUnlocked(FishData fish)
    {
        return GetCountForFish(fish) >= _fishCountForIcon;
    }

    public bool IsSizeUnlocked(FishData fish)
    {
        return GetCountForFish(fish) >= _fishCountForSize;
    }
    
    public override void Uninitialize()
    {        
        _fishCounts.Clear();
        Debug.Log("[FishingCodex] Uninitialized");
    }
}
