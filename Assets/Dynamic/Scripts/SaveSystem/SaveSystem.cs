using System;
using System.IO;
using System.Collections.Generic;
using Manager.Fishs;
using Manager.Lure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Player;
using Save.Data;
using Singletons;
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
        private DialogueLauncher _tutorialDialogueLauncher;

        // Dernière position connue du joueur (même si PlayerMovements n'est pas présent dans la scène)
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
            DialogueLauncher[] allDialogueLaunchers = FindObjectsByType<DialogueLauncher>(FindObjectsSortMode.None);
            _tutorialDialogueLauncher = Array.Find(allDialogueLaunchers, dialogueLauncher => !dialogueLauncher.IsReusable);
            
            // initialise
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

            if (scene.name != "ile_CETTI")
                return;

            if (File.Exists(_filePath))
            {
                Load();
            }
            else
            {
                Debug.LogWarning($"SaveSystem: aucun fichier de sauvegarde trouvé à {_filePath} lors de l'entrée dans {scene.name}.");
            }
        }

        /// <summary>
        /// Called by Player when it is available. We record the reference and apply last known position if we have one.
        /// </summary>
        public void SetPlayerMovements(PlayerMovements playerMovements)
        {
            _playerMovements = playerMovements;

            // Applique la dernière position connue si on en a une
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

        /// <summary>
        /// Permet aux autres scripts (ex: PlayerMovements) d'indiquer la position actuelle du joueur.
        /// Utile pour garder une position quand le Player n'est pas présent dans la scène où on veut sauvegarder.
        /// </summary>
        public void UpdatePlayerPosition(Vector3 pos)
        {
            _lastPlayerPosition = new SerializableVector3(pos);
            _hasLastPlayerPosition = true;
        }

        [ContextMenu("Save")]
        public void Save()
        {
            if (_fishsManager == null)
            {
                Debug.LogWarning("SaveSystem: FishsManager reference is null. totalFishCount will be 0.");
            }
            if (_lureManager == null)
            {
                Debug.LogWarning("SaveSystem: LureManager reference is null. countByLure will be empty.");
            }
            if (_fishingCodex == null)
            {
                Debug.LogWarning("SaveSystem: FishingCodex reference is null. countByFish will be empty.");
            }

            // Utilise la position du player s'il existe sinon fallback sur la dernière position connue (ou zéro)
            SerializableVector3 positionToSave;
            if (_playerMovements != null)
            {
                Debug.LogWarning("Hello");
                positionToSave = new SerializableVector3(_playerMovements.transform.position);
            }
            else if (_hasLastPlayerPosition)
                positionToSave = _lastPlayerPosition;
            else
                positionToSave = new SerializableVector3(Vector3.zero);

            SaveData data = new()
            {
                playerPosition = positionToSave,
                totalFishCount = _fishsManager != null ? _fishsManager.FishsCount : 0,
                countByLure = _lureManager != null ? _lureManager.Serialize() : new Dictionary<int, int>(),
                countByFish = _fishingCodex != null ? _fishingCodex.Serialize() : new Dictionary<int, int>()
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
                Debug.Log($"Save Succeed : {_filePath}");
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
                    Debug.LogError($"JSON Deserialisation Error: Could not find file \"{_filePath}\"");
                    return;
                }

                using StreamReader reader = new(_filePath);
                string json = reader.ReadToEnd();

                SaveData data = JsonConvert.DeserializeObject<SaveData>(json);

                if (data == null)
                {
                    Debug.LogError("SaveSystem: Loaded SaveData is null.");
                    return;
                }

                // Sauvegarde la position chargée dans _lastPlayerPosition (au cas où le player n'est pas encore présent)
                _lastPlayerPosition = data.playerPosition;
                _hasLastPlayerPosition = true;

                if (_playerMovements != null)
                {
                    Debug.LogWarning(data.playerPosition.ToVector3());
                    // Utiliser ApplyLoadedPosition pour s'assurer que le rigidbody est mis à jour proprement
                    try
                    {
                        _playerMovements.ApplyLoadedPosition(data.playerPosition.ToVector3());
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"SaveSystem: failed to apply loaded position via ApplyLoadedPosition: {ex.Message}");
                        _playerMovements.transform.position = data.playerPosition.ToVector3();
                    }
                }
                else
                    Debug.LogWarning("SaveSystem: PlayerMovements reference is null when loading - player position saved for later application.");

                if (_fishsManager != null)
                    _fishsManager.FishsCount = data.totalFishCount;
                else
                    Debug.LogWarning("SaveSystem: FishsManager reference is null when loading - totalFishCount not applied.");

                if (_lureManager != null)
                    _lureManager.Deserialize(data.countByLure);
                else
                    Debug.LogWarning("SaveSystem: LureManager reference is null when loading - countByLure not applied.");

                if (_fishingCodex != null)
                    _fishingCodex.Deserialize(data.countByFish);
                else
                    Debug.LogWarning("SaveSystem: FishingCodex reference is null when loading - countByFish not applied.");

                Debug.Log($"Load Succed : {_filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Deserialisation Error: {ex.Message}");
            }
        }

        // private void OnApplicationQuit()
        // {
        //     Save();
        // }
    }
}
