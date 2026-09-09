using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace DeadZone.Core.Managers
{
    /// <summary>
    /// Manages game save/load functionality.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }

        private string savePath;
        private const string SAVE_FOLDER = "DeadZoneSaves";
        private const string SAVE_FILE_EXTENSION = ".json";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Path.Combine(Application.persistentDataPath, SAVE_FOLDER);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        public void SaveGameData(string fileName, GameSaveData data)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                string filePath = Path.Combine(savePath, fileName + SAVE_FILE_EXTENSION);
                File.WriteAllText(filePath, json);
                Debug.Log($"Game saved to: {filePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to save game: {ex.Message}");
            }
        }

        public GameSaveData LoadGameData(string fileName)
        {
            try
            {
                string filePath = Path.Combine(savePath, fileName + SAVE_FILE_EXTENSION);
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning($"Save file not found: {filePath}");
                    return null;
                }

                string json = File.ReadAllText(filePath);
                GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
                Debug.Log($"Game loaded from: {filePath}");
                return data;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load game: {ex.Message}");
                return null;
            }
        }

        public bool SaveFileExists(string fileName)
        {
            string filePath = Path.Combine(savePath, fileName + SAVE_FILE_EXTENSION);
            return File.Exists(filePath);
        }

        public void DeleteSaveFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine(savePath, fileName + SAVE_FILE_EXTENSION);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Debug.Log($"Save file deleted: {filePath}");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to delete save file: {ex.Message}");
            }
        }

        public List<string> GetAvailableSaves()
        {
            List<string> saves = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(savePath, "*" + SAVE_FILE_EXTENSION);
                foreach (string file in files)
                {
                    saves.Add(Path.GetFileNameWithoutExtension(file));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to get available saves: {ex.Message}");
            }
            return saves;
        }
    }

    [System.Serializable]
    public class GameSaveData
    {
        public float playerHealth;
        public float playerStamina;
        public float playerArmor;
        public int playerLevel;
        public int playerXP;
        public int currency;
        public Vector3 playerPosition;
        public string currentMission;
        public int waveNumber;
        public float timeSurvived;
        public InventorySaveData inventoryData;
    }

    [System.Serializable]
    public class InventorySaveData
    {
        public int[] itemIds;
        public int[] itemCounts;
        public int currentWeaponIndex;
    }
}
