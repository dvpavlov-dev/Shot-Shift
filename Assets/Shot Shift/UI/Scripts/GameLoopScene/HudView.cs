using Shot_Shift.Infrastructure.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class HudView : WindowView
    {
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private Image _bulletsImage;
        [SerializeField] private TMP_Text _bulletsText;

        [SerializeField] private RectTransform _bulletTimeButton;
        [SerializeField] private RectTransform _switchWeaponButton;
        [SerializeField] private RectTransform _attackButton;
        [SerializeField] private RectTransform _joystickArea;
        
        private SettingsService _settingsService;

        private bool _wasInverted;

        [Inject]
        private void Constructor(SettingsService settingsService)
        {
            _settingsService = settingsService;

            _settingsService.OnInvertControlStatusChanged += SyncInvertControl;
        }
        
        public void SetupHud(float maxHealth, int timerSeconds)
        {
            _healthBarView.SetupHealthBar(maxHealth);
            _timerView.SetupTimer(timerSeconds);
            
            SyncInvertControl();
        }
        
        public void UpdateHealth(float health)
        {
            _healthBarView.UpdateHealthBar(health);
        }

        public void UpdateTimer(int seconds)
        {
            _timerView.UpdateTimer(seconds);
        }

        public void UpdateCoins(int coins)
        {
            _coinsText.text = coins.ToString();
        }

        private void SyncInvertControl()
        {
            if (_settingsService.InvertControlStatus)
            {
                InvertControl(_bulletTimeButton);
                InvertControl(_switchWeaponButton);
                InvertControl(_attackButton);
                InvertControl(_joystickArea);
            }
            else
            {
                NormalControl(_bulletTimeButton);
                NormalControl(_switchWeaponButton);
                NormalControl(_attackButton);
                NormalControl(_joystickArea);
            }
        }
        
        private void InvertControl(RectTransform target)
        {
            _wasInverted = true;
            
            if(target.anchorMax.y == 0)
            {
                float posX = target.anchoredPosition3D.x;
                target.anchorMax = target.anchorMin = Vector2.zero;
                target.anchoredPosition3D = new Vector3(posX < 0 ? -posX : posX, target.anchoredPosition3D.y, target.anchoredPosition3D.z);
            }
            else
            {
                float newLeft = Mathf.Abs(target.offsetMax.x);
                float newRight = Mathf.Abs(target.offsetMin.x);
                
                target.offsetMin = new Vector2(newLeft, target.offsetMin.y);
                target.offsetMax = new Vector2(newRight, target.offsetMax.y);
            }
        }
        
        private void NormalControl(RectTransform target)
        {
            if(target.anchorMax.y == 0)
            {
                float posX = target.anchoredPosition3D.x;
                target.anchorMax = target.anchorMin = new Vector2(1, 0);
                target.anchoredPosition3D = new Vector3(posX > 0 ? -posX : posX, target.anchoredPosition3D.y, target.anchoredPosition3D.z);
            }
            else
            {
                float newLeft = _wasInverted ? Mathf.Abs(target.offsetMax.x) : Mathf.Abs(target.offsetMin.x);
                float newRight = _wasInverted ? Mathf.Abs(target.offsetMin.x) : Mathf.Abs(target.offsetMax.x);
                
                target.offsetMin = new Vector2(newLeft, target.offsetMin.y);
                target.offsetMax = new Vector2(-newRight, target.offsetMax.y);
            }
        }

        private void OnDestroy()
        {
            _settingsService.OnInvertControlStatusChanged -= SyncInvertControl;
        }
    }
}
