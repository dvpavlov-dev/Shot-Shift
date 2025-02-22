using UnityEngine;
using UnityEngine.UI;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private float _maxHealth;
        private bool _isLookAtCameraNeeded;

        public void SetupHealthBar(float maxHealth, bool isLookAtCameraNeeded = false)
        {
            _isLookAtCameraNeeded = isLookAtCameraNeeded;
            _maxHealth = maxHealth;
        }
        
        public void UpdateHealthBar(float health)
        {
            if (health > _maxHealth)
            {
                health = _maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
            }
            
            _healthBar.fillAmount = health / _maxHealth;
        }

        private void LateUpdate()
        {
            if (_isLookAtCameraNeeded && Camera.main is {} cameraMain)
            {
                transform.rotation = Quaternion.LookRotation(cameraMain.transform.forward);
            }
        }
    }
}
