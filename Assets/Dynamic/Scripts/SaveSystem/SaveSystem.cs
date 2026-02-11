using System;
using System.IO;
using Manager.Fishs;
using Manager.Lure;
using Newtonsoft.Json;
using Player;
using Save.Data;
using Singletons;
using UnityEngine;

namespace Save
{
    public class SaveSystem : PersistentMonoSingleton<SaveSystem>
    {
        private PlayerMovements _playerMovements;
        private FishsManager _fishsManager;
        private LureManager _lureManager;
        private FishingCodex _fishingCodex;
        
        public string _filePath = $"{Application.persistentDataPath}/SaveData.json";
        
        protected override void Awake()
        {
            base.Awake();
            
            _playerMovements = (PlayerMovements)FindFirstObjectByType(typeof(PlayerMovements));
            _fishsManager = (FishsManager)FindFirstObjectByType(typeof(FishsManager));
            _lureManager = (LureManager)FindFirstObjectByType(typeof(LureManager));
            _fishingCodex = (FishingCodex)FindFirstObjectByType(typeof(FishingCodex));
        }

        [ContextMenu("Save")]
        public void Save()
        {
            SaveData data = new()
            {
                playerPosition = _playerMovements.transform.position,
                totalFishCount = _fishsManager.FishsCount,
                countByLure = _lureManager.Serialize(),
                countByFish = _fishingCodex.Serialize()
            };
            
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                using FileStream stream = new(_filePath, FileMode.Create);
                using StreamWriter writer = new(stream);
                writer.Write(json);
                Debug.Log($"Save Succed : {_filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Serialisation Error: {ex.Message}");
            }
        }

        [ContextMenu("Load")]
        public void Load()
        {
            try
            {
                using StreamReader reader = new(_filePath);
                string json = reader.ReadToEnd();
                
                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
            
                _lureManager.Deserialize(data.countByLure);
                _fishingCodex.Deserialize(data.countByFish);
                
                Debug.Log($"Load Succed : {_filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Deserialisation Error: {ex.Message}");
            }
        }
    }
}
