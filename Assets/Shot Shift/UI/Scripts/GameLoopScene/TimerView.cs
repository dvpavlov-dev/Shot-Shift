using TMPro;
using UnityEngine;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        
        public void SetupTimer(int seconds)
        {
            if (seconds <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _timerText.text = seconds.ToString();
            }
        }

        public void UpdateTimer(int seconds)
        {
            _timerText.text = seconds.ToString();
        }
    }
}
