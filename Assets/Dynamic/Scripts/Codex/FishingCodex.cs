using System.Collections.Generic;
using Singletons;
using UnityEngine;

public class FishingCodex : PersistentMonoSingleton<FishingCodex>
{
    [Header("Configuration")]
    [Tooltip("Nombre de poissons à pêcher pour révéler le nom")]
    [SerializeField] private int _fishCountForName = 1;

    [Tooltip("Nombre de poissons à pêcher pour révéler l'icône")]
    [SerializeField] private int _fishCountForIcon = 3;

    [Tooltip("Nombre de poissons à pêcher pour révéler la taille")]
    [SerializeField] private int _fishCountForSize = 5;
    
    [Tooltip("Nombre de poissons à pêcher pour révéler la nourriture")]
    [SerializeField] private int _fishCountForFood = 7;
    
    
    [Tooltip("Nombre de poissons à pêcher pour révéler la description")]
    [SerializeField] private int _fishCountForDescription = 12;

    private Dictionary<FishData, int> _fishCounts = new();
    
    [SerializeField] private FishData[] _fishDatabase;
    
    public int FishCountForName => _fishCountForName;
    public int FishCountForDescription => _fishCountForDescription;
    public int FishCountForIcon => _fishCountForIcon;
    public int FishCountForSize => _fishCountForSize;
    public int FishCountForFood => _fishCountForFood;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
    
    public void RecordFishCatch(FishData fish)
    {
        if (!_fishCounts.TryAdd(fish, 1))
            _fishCounts[fish]++;
    }

    public int GetCountForFish(FishData fish)
    {
        return _fishCounts.GetValueOrDefault(fish);
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
    
    public bool IsFoodUnlocked(FishData fish)
    {
        return GetCountForFish(fish) >= _fishCountForFood;
    }
    
    public bool IsFishFullyUnlocked(FishData fish)
    {
        return IsNameUnlocked(fish) && IsDescriptionUnlocked(fish) && IsIconUnlocked(fish) && IsSizeUnlocked(fish) && IsFoodUnlocked(fish);
    }
    
    public bool IsAllFishFullyUnlocked()
    {
        foreach (FishData fish in _fishDatabase)
        {
            if (!IsFishFullyUnlocked(fish))
                return false;
        }
        return true;
    }

    public Dictionary<int, int> Serialize()
    {
        Dictionary<int, int> serializedData = new();
        
        foreach (KeyValuePair<FishData, int> countByLureDataItem in _fishCounts)
        {
            int lureId = System.Array.IndexOf(_fishDatabase, countByLureDataItem.Key);
            if (lureId >= 0)
            {
                serializedData[lureId] = countByLureDataItem.Value;
            }
        }
        
        return serializedData;
    }

    public void Deserialize(Dictionary<int, int> serializedData)
    {
        _fishCounts.Clear();
            
        foreach (KeyValuePair<int, int> item in serializedData)
        {
            if (item.Key >= 0 && item.Key < _fishDatabase.Length)
            {
                FishData lure = _fishDatabase[item.Key];
                _fishCounts[lure] = item.Value;
            }
        }
    }
    
    public override void Uninitialize()
    {        
        _fishCounts.Clear();
    }
}
