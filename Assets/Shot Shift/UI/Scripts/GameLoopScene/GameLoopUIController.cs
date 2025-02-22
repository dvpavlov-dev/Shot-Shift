using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.UI.Scripts.StartScene;
using UnityEngine;
using Zenject;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class GameLoopUIController : MonoBehaviour
    {
        [SerializeField] private List<WindowView> _windows;
        
        private PauseService _pauseService;

        [Inject]
        private void Constructor(PauseService pauseService)
        {
            _pauseService = pauseService;
        }
        
        public void SelectWindow(WindowView window)
        {
            HideAllWindow();
            window.ShowWindow();
        }

        public void SetPause(bool isPaused)
        {
            _pauseService.IsPaused = isPaused;
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
