using System;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class PlayerProgressService : IInitializable
    {
        private Configs _configs;
        private const string CURRENT_LEVEL = "CurrentLevel";
        private const string LAST_COMPLETED_LEVEL = "LastCompletedLevel";
        private const string COINS_COUNT = "CoinsCount";
        private const string HEALTH_UPGRADE = "HealthUpgrade";
        private const string DAMAGE_UPGRADE = "DamageUpgrade";
        private const string RECOIL_UPGRADE = "RecoilUpgrade";
        private const int MAX_DAMAGE_LEVEL = 10;
        private const int MAX_RECOIL_LEVEL = 10;
        private const int MAX_HEALTH_LEVEL = 10;

        public int CurrentLevel { get; private set; }
        public int LastCompletedLevel { get; private set; }
        public int CoinsCount { get; private set; }
        
        public float DamageUpgrade => 1 + _damageLevel * 2 / 10f;
        public float RecoilUpgrade => 1 + _recoilLevel * 2 / 10f;
        public float HealthUpgrade => 1 + _healthLevel * 2 / 10f;

        public int DamageLevelCost => _damageLevel * 50;
        public int RecoilLevelCost => _recoilLevel * 50;
        public int HealthLevelCost => _healthLevel * 50;
        
        public bool IsDamageLevelMax => _damageLevel == MAX_DAMAGE_LEVEL;
        public bool IsRecoilLevelMax => _recoilLevel == MAX_RECOIL_LEVEL;
        public bool IsHealthLevelMax => _healthLevel == MAX_HEALTH_LEVEL;
        
        private int _damageLevel = 1;
        private int _recoilLevel = 1;
        private int _healthLevel = 1;

        public event Action OnCoinsChanged;
        public event Action OnUpgradesChanged;

        [Inject]
        private void Construct(Configs configs)
        {
            _configs = configs;
        }
        
        public void Initialize()
        {
            LoadData();
        }

        public void ChangeLevelData(int currentLevel)
        {
            if (_configs.LevelsConfig.Levels.Count <= currentLevel)
            {
                currentLevel = _configs.LevelsConfig.Levels.Count - 1;
            }
            
            CurrentLevel = currentLevel;

            if (currentLevel > LastCompletedLevel)
            {
                LastCompletedLevel = currentLevel;
            }
            
            SaveData();
        }

        public void AddCoins(int value)
        {
            CoinsCount += value;
            OnCoinsChanged?.Invoke();

            SaveData();
        }

        public bool TryReduceCoinsData(int value)
        {
            if(CoinsCount - value >= 0)
            {
                CoinsCount -= value;
                OnCoinsChanged?.Invoke();

                SaveData();
                
                return true;
            }
            
            return false;
        }

        public void IncreaseDamageLevel()
        {
            _damageLevel++;
            OnUpgradesChanged?.Invoke();
            
            SaveData();
        }
        
        public void IncreaseRecoilLevel()
        {
            _recoilLevel++;
            OnUpgradesChanged?.Invoke();
            
            SaveData();
        }
        
        public void IncreaseHealthLevel()
        {
            _healthLevel++;
            OnUpgradesChanged?.Invoke();
            
            SaveData();
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, CurrentLevel);
            PlayerPrefs.SetInt(LAST_COMPLETED_LEVEL, LastCompletedLevel);
            PlayerPrefs.SetInt(COINS_COUNT, CoinsCount);
            PlayerPrefs.SetInt(DAMAGE_UPGRADE, _damageLevel);
            PlayerPrefs.SetInt(RECOIL_UPGRADE, _recoilLevel);
            PlayerPrefs.SetInt(HEALTH_UPGRADE, _healthLevel);

            PlayerPrefs.Save();
            
            Debug.Log("Data saved");
        }

        private void LoadData()
        {
            // CurrentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL);
            // LastCompletedLevel = PlayerPrefs.GetInt(LAST_COMPLETED_LEVEL);
            // CoinsCount = PlayerPrefs.GetInt(COINS_COUNT);
            // _damageLevel = Math.Max(PlayerPrefs.GetInt(DAMAGE_UPGRADE), 1);
            // _recoilLevel = Math.Max(PlayerPrefs.GetInt(RECOIL_UPGRADE), 1);
            // _healthLevel = Math.Max(PlayerPrefs.GetInt(HEALTH_UPGRADE), 1);
            
            CurrentLevel = 0;
            LastCompletedLevel = 4;
            CoinsCount = 10000;
            _damageLevel = 1;
            _recoilLevel = 1;
            _healthLevel = 1;
            
            Debug.Log("Data loaded");
        }
    }
}
