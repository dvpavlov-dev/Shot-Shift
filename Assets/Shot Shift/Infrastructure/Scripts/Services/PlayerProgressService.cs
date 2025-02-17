using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class PlayerProgressService : IInitializable
    {
        private const string CURRENT_LEVEL = "CurrentLevel";
        private const string COINS_COUNT = "CoinsCount";
        private const string HEALTH_UPGRADE = "HealthUpgrade";
        private const string DAMAGE_UPGRADE = "DamageUpgrade";
        private const string RECOIL_UPGRADE = "RecoilUpgrade";
        
        public int CurrentLevel { get; private set; }
        public int CoinsCount { get; private set; }
        public float HealthUpgrade { get; private set; }
        public float DamageUpgrade { get; private set; }
        public float RecoilUpgrade { get; private set; }
        
        public void Initialize()
        {
            LoadData();
        }

        public void ChangeLevelData(int currentLevel)
        {
            CurrentLevel = currentLevel;
        }

        public void ChangeCoinsData(int coins)
        {
            CoinsCount = coins;
        }

        public void ChangeUpgradeData(float healthUpgrade, float damageUpgrade, float recoilUpgrade)
        {
            HealthUpgrade = healthUpgrade;
            DamageUpgrade = damageUpgrade;
            RecoilUpgrade = recoilUpgrade;
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, CurrentLevel);
            PlayerPrefs.SetInt(COINS_COUNT, CoinsCount);
            PlayerPrefs.SetFloat(HEALTH_UPGRADE, HealthUpgrade);
            PlayerPrefs.SetFloat(DAMAGE_UPGRADE, DamageUpgrade);
            PlayerPrefs.SetFloat(RECOIL_UPGRADE, RecoilUpgrade);
            
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            CurrentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL);
            CoinsCount = PlayerPrefs.GetInt(COINS_COUNT);
            HealthUpgrade = PlayerPrefs.GetFloat(HEALTH_UPGRADE);
            DamageUpgrade = PlayerPrefs.GetFloat(DAMAGE_UPGRADE);
            RecoilUpgrade = PlayerPrefs.GetFloat(RECOIL_UPGRADE);
        }
    }
}
