/*using System;
using System.IO;
using UnityEngine;
using Char = ImportantScripts.CharScripts.Char;

namespace ImportantScripts.Saves
{
    [System.Serializable]
    public class PlayerSaves : MonoBehaviour
    {
        public static PlayerSaves Instance;
        public GameSlot localData;
        public GameSlot defaultData;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void LoadGame()
        {
            string filePath = Path.Combine(Application.persistentDataPath, "Save1");
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);

                GameSlot localRef = JsonUtility.FromJson<GameSlot>(dataAsJson);

                localData = localRef;
            }

            else
            {
                DefaultSaveGame();
                LoadGame();
            }
        }

        public void SaveGame()
        {
            GameSlot localRef = localData;

            string dataToSave = JsonUtility.ToJson(localRef);
            string filePath = Path.Combine(Application.persistentDataPath, "Save1");
            File.WriteAllText(filePath,dataToSave);
        }

        private void OnApplicationQuit()
        {
            localData.playerPos = Char.CharIn.transform.position;
            
            SaveGame();
        }

        public void DefaultSaveGame()
        {
             GameSlot localRef = defaultData;
            
             string dataToSave = JsonUtility.ToJson(localRef);
             string filePath = Path.Combine(Application.persistentDataPath, "Save1");
             File.WriteAllText(filePath,dataToSave);
        }
    }

    [System.Serializable]
    public class GameSlot
    {
        public Vector3 playerPos;
    }
}
*/