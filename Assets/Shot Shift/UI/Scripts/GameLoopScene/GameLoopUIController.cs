using System.Collections.Generic;
using Shot_Shift.UI.Scripts.StartScene;
using UnityEngine;

namespace Shot_Shift.UI.Scripts
{
    public class GameLoopUIController : MonoBehaviour
    {
        [SerializeField] private List<WindowView> _windows;
        
        public void SelectWindow(WindowView window)
        {
            HideAllWindow();
            window.ShowWindow();
        }
        
        public void ShowLostWindow()
        {
            
        }

        public void ShowWinningWindow()
        {
            
        }
        
        private void HideAllWindow()
        {
            foreach (WindowView windowView in _windows)
            {
                windowView.HideWindow();
            }
        }
    }
}
