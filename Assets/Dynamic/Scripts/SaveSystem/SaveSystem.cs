using System;
using System.IO;
using System.Collections.Generic;
using Inventory;
using Manager.Fishs;
using Manager.Lure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Player;
using Save.Data;
using Singletons;
using Tutorial.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public class SaveSystem : PersistentMonoSingleton<SaveSystem>
    {
        [Header("References (can be assigned in inspector)")]
        private PlayerMovements _playerMovements;
        private FishsManager _fishsManager;
        private LureManager _lureManager;
        private FishingCodex _fishingCodex;
        private TutorialManager _tutorialManager;
        private InventoryManager _inventoryManager;

        private SerializableVector3 _lastPlayerPosition;
        private bool _hasLastPlayerPosition;

        public string _filePath;

        protected override void Awake()
        {
            base.Awake();

            _filePath = $"{Application.persistentDataPath}/SaveData.json";

            _playerMovements = (PlayerMovements)FindFirstObjectByType(typeof(PlayerMovements));
            _fishsManager = (FishsManager)FindFirstObjectByType(typeof(FishsManager));
            _lureManager = (LureManager)FindFirstObjectByType(typeof(LureManager));
            _fishingCodex = (FishingCodex)FindFirstObjectByType(typeof(FishingCodex));
            _tutorialManager = (TutorialManager)FindFirstObjectByType(typeof(TutorialManager));
            _inventoryManager = (InventoryManager)FindFirstObjectByType(typeof(InventoryManager));
            // DialogueLauncher[] allDialogueLaunchers = FindObjectsByType<DialogueLauncher>(FindObjectsSortMode.None);
            // _tutorialDialogueLauncher = Array.Find(allDialogueLaunchers, dialogueLauncher => !dialogueLauncher.IsReusable);
            
            _lastPlayerPosition = new SerializableVector3(Vector3.zero);
            _hasLastPlayerPosition = false;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_playerMovements == null)
                _playerMovements = (PlayerMovements)FindFirstObjectByType(typeof(PlayerMovements));
            if (_fishsManager == null)
                _fishsManager = (FishsManager)FindFirstObjectByType(typeof(FishsManager));
            if (_lureManager == null)
                _lureManager = (LureManager)FindFirstObjectByType(typeof(LureManager));
            if (_fishingCodex == null)
                _fishingCodex = (FishingCodex)FindFirstObjectByType(typeof(FishingCodex));
            if(_tutorialManager == null)
                _tutorialManager = (TutorialManager)FindFirstObjectByType(typeof(TutorialManager));
            if(_inventoryManager == null)
                _inventoryManager = (InventoryManager)FindFirstObjectByType(typeof(InventoryManager));

            if (scene.name != "ile_CETTI")
                return;

            if (File.Exists(_filePath))
            {
                Load();
            }
        }

        public void SetPlayerMovements(PlayerMovements playerMovements)
        {
            _playerMovements = playerMovements;

            if (_playerMovements != null && _hasLastPlayerPosition)
            {
                try
                {
                    _playerMovements.ApplyLoadedPosition(_lastPlayerPosition.ToVector3());
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"SaveSystem: impossible d'appliquer la position sauvegardée au player: {ex.Message}");
                }
            }
        }

        public void UpdatePlayerPosition(Vector3 pos)
        {
            _lastPlayerPosition = new SerializableVector3(pos);
            _hasLastPlayerPosition = true;
        }

        [ContextMenu("Save")]
        public void Save()
        {
            SerializableVector3 positionToSave;
            if (_playerMovements)
            {
                positionToSave = new SerializableVector3(_playerMovements.transform.position);
            }
            else if (_hasLastPlayerPosition)
                positionToSave = _lastPlayerPosition;
            else
                positionToSave = new SerializableVector3(Vector3.zero);

            SaveData data = new()
            {
                playerPosition = positionToSave,
                totalFishCount = _fishsManager ? _fishsManager.FishsCount : 0,
                countByLure = _lureManager ? _lureManager.Serialize() : new Dictionary<int, int>(),
                countByFish = _fishingCodex ? _fishingCodex.Serialize() : new Dictionary<int, int>(),
                isFirstTutorialFinished = _tutorialManager && _tutorialManager.IsFirstTutorialFinished,
                isTutorialDialogueFinished = _tutorialManager && _tutorialManager.IsTutorialFinished,
                isInventoryTutorialFinished = _inventoryManager && _inventoryManager.IsTutorialFinished
            };
            
            try
            {
                JsonSerializerSettings settings = new()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented,
                    ContractResolver = new DefaultContractResolver()
                };

                string json = JsonConvert.SerializeObject(data, settings);
                using FileStream stream = new(_filePath, FileMode.Create);
                using StreamWriter writer = new(stream);
                writer.Write(json);
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
                if (!File.Exists(_filePath))
                {
                    return;
                }

                using StreamReader reader = new(_filePath);
                string json = reader.ReadToEnd();

                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);

                if (data == null)
                {
                    return;
                }

                _lastPlayerPosition = data.playerPosition;
                _hasLastPlayerPosition = true;

                if (_playerMovements != null)
                {
                    try
                    {
                        _playerMovements.ApplyLoadedPosition(data.playerPosition.ToVector3());
                    }
                    catch (Exception)
                    {
                        _playerMovements.transform.position = data.playerPosition.ToVector3();
                    }
                }
                

                if (_fishsManager != null)
                    _fishsManager.FishsCount = data.totalFishCount;

                if (_lureManager != null)
                    _lureManager.Deserialize(data.countByLure);

                if (_fishingCodex != null)
                    _fishingCodex.Deserialize(data.countByFish);

                if (_tutorialManager != null)
                {
                    _tutorialManager.IsFirstTutorialFinished = data.isFirstTutorialFinished;
                    _tutorialManager.IsTutorialFinished = data.isTutorialDialogueFinished;
                }
                if(_inventoryManager != null)
                    _inventoryManager.IsTutorialFinished = data.isInventoryTutorialFinished;
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Deserialisation Error: {ex.Message}");
            }
        }
    }
}
