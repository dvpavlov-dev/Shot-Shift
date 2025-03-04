using Shot_Shift.Infrastructure.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Shot_Shift.UI.Scripts.StartScene
{
    public class UpgradesWindow : WindowView
    {
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _damageCostText;
        [SerializeField] private TMP_Text _recoilCostText;
        [SerializeField] private TMP_Text _healthCostText;
        [SerializeField] private TMP_Text _damageProcessText;
        [SerializeField] private TMP_Text _recoilProcessText;
        [SerializeField] private TMP_Text _healthProcessText;
        
        private PlayerProgressService _playerProgressService;

        [Inject]
        private void Constructor(PlayerProgressService playerProgressService)
        {
            _playerProgressService = playerProgressService;

            _playerProgressService.OnCoinsChanged += UpdateCoinsCount;
            _playerProgressService.OnUpgradesChanged += UpdateUpgradesText;
        }

        private void OnEnable()
        {
            UpdateCoinsCount();
            UpdateUpgradesText();
        }

        public void DamageUpgradeSelected()
        {
            if(!_playerProgressService.IsDamageLevelMax)
            {
                if(_playerProgressService.TryReduceCoinsData(_playerProgressService.DamageLevelCost))
                {
                    _playerProgressService.IncreaseDamageLevel();
                }
            }
            else
            {
                _damageProcessText.text = "MAX";
            }
        }

        public void RecoilUpgradeSelected()
        {
            if(!_playerProgressService.IsRecoilLevelMax)
            {
                if(_playerProgressService.TryReduceCoinsData(_playerProgressService.RecoilLevelCost))
                {
                    _playerProgressService.IncreaseRecoilLevel();
                }
            }
            else
            {
                _recoilProcessText.text = "MAX";
            }
        }
        
        public void HealthUpgradeSelected()
        {
            if(!_playerProgressService.IsHealthLevelMax)
            {
                if(_playerProgressService.TryReduceCoinsData(_playerProgressService.HealthLevelCost))
                {
                    _playerProgressService.IncreaseHealthLevel();
                }
            }
            else
            {
                _healthProcessText.text = "MAX";
            }
        }
        
        private void UpdateCoinsCount()
        {
            _coinsText.text = _playerProgressService.CoinsCount.ToString();
        }
        
        private void UpdateUpgradesText()
        {
            _damageCostText.text =  _playerProgressService.IsDamageLevelMax ? "MAX" : _playerProgressService.DamageLevelCost.ToString();
            _recoilCostText.text =  _playerProgressService.IsRecoilLevelMax ? "MAX" : _playerProgressService.RecoilLevelCost.ToString();
            _healthCostText.text =  _playerProgressService.IsHealthLevelMax ? "MAX" : _playerProgressService.HealthLevelCost.ToString();
            
            _damageProcessText.text = _playerProgressService.IsDamageLevelMax ? "MAX" : $"{(_playerProgressService.DamageUpgrade - 1) * 100}%";
            _recoilProcessText.text = _playerProgressService.IsRecoilLevelMax ? "MAX" : $"{(_playerProgressService.RecoilUpgrade - 1) * 100}%";
            _healthProcessText.text = _playerProgressService.IsHealthLevelMax ? "MAX" : $"{(_playerProgressService.HealthUpgrade - 1) * 100}%";
        }
        
        private void OnDestroy()
        {
            if (SceneManager.sceneCount == 0) return;
            
            _playerProgressService.OnCoinsChanged -= UpdateCoinsCount;
            _playerProgressService.OnUpgradesChanged -= UpdateUpgradesText;
        }
    }
}
