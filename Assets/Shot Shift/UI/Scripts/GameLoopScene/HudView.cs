using Shot_Shift.UI.Scripts.StartScene;
using UnityEngine;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class HudView : WindowView
    {
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private TimerView _timerView;

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
    }
}
