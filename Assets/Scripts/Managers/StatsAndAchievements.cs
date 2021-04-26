using System.Collections.Generic;

namespace DefaultNamespace.Managers
{
    public class StatsAndAchievements
    {
        //Game Data
        private static StatsData statsData = new StatsData();
        private static SettingsData settingsData;

        public static int Coins { get { return statsData.coins; } set { statsData.coins = value; } }

        #region Data Management
        public static void LoadSettingsData(bool isMusicMuted, bool isSFXMuted)
        {
            settingsData = new SettingsData(isMusicMuted, isSFXMuted);
        }

        public static SettingsData GetSettingsData() => settingsData;

        public static void LoadStatsData(int coins)
        {
            
        }
        public static StatsData GetStatsData() => statsData;
        public static SaveData GetSaveData()
        {
            return new SaveData(
                GetSettingsData(),
                GetStatsData());
        }
        #endregion

        #region Coin Modifying
        public static bool CanPurchase(int purchasePrice)
        {
            if (Coins >= purchasePrice)
            {
                Coins -= purchasePrice;
                return true;
            }
            return false;
        }
        #endregion

        #region Changing Data
        
        #endregion
    }
}