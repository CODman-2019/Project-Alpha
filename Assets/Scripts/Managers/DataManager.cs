using System.IO;
using UnityEngine;

namespace DefaultNamespace.Managers
{
    public class DataManager
    {
        private const string defaultFilePath = "/default.txt";

        public static void SaveData()
        {
            SaveData data = StatsAndAchievements.GetSaveData();
        }
        public static void ResetData()
        {
    
        }
    
        private static void DeleteFile(string filePath)
        {
            if (File.Exists(Application.persistentDataPath + filePath))
            {
                File.Delete(Application.persistentDataPath + filePath);
            }
        }
        private static void WriteToFile(string filePath, string stringToWrite)
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + filePath, false);

            sw.Write(stringToWrite);

            sw.Close();
        }
        public static bool LoadData()
        {
            //Checks if the map file exits
            if (File.Exists(Application.persistentDataPath + defaultFilePath))
            {
                //Reads the whole file
                var lines = File.ReadAllLines(Application.persistentDataPath + defaultFilePath);

                //Iterates thru all of the lines in the file
                for (var i = 0; i < lines.Length; i++)
                {
                    //If the line is empty, break out of the loop
                    if (lines[i] == "") break;

                    //Splits the line 
                    var splitLine = lines[i].Split(';');

                    //Parse The Data
                }
            }
        
            return true;
        }
    }
    #region Structs
    public struct SaveData
    {
        public string settingsInfo;
        public string statsInfo;

        public SaveData(SettingsData settingsData, StatsData statsData)
        {
            settingsInfo = settingsData.GetSaveInfo();
            statsInfo = statsData.GetSaveInfo();
        }
    }
    public struct SettingsData
    {
        public bool isMusicMuted;
        public bool isSFXMuted;

        public SettingsData(bool isMusicMuted, bool isSFXMuted)
        {
            this.isMusicMuted = isMusicMuted;
            this.isSFXMuted = isSFXMuted;
        }
        public string GetSaveInfo()
        {
            return (isMusicMuted ? 1 : 0) + ";" + (isSFXMuted ? 1 : 0);
        }
    }
    public struct StatsData
    {
        public int coins;

        public StatsData(int coins)
        {
            this.coins = coins;
        }
        public string GetSaveInfo()
        {
            return coins.ToString();
        }
    }
    #endregion
}