using Shot_Shift.UI.Scripts.StartScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class EndRoundWindow : WindowView
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _backgroundTitleImage;

        private bool _isWinWindow;

        public void ShowWinWindow()
        {
            _isWinWindow = true;
        }

        public void ShowLoseWindow()
        {
            _isWinWindow = false;
        }

        public void OnContinueSelected()
        {
            
        }
    }
}
