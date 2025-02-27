using TMPro;
using UnityEngine;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class HudView : WindowView
    {
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private TMP_Text _coinsText;

        public void SetupHud(float maxHealth, int timerSeconds)
        {
            _healthBarView.SetupHealthBar(maxHealth);
            _timerView.SetupTimer(timerSeconds);
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
    }
}
