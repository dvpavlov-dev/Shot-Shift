using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class GameLoopUIController : MonoBehaviour
    {
        public HudView HudView => _hudView;
        
        [SerializeField] private HudView _hudView;
        [SerializeField] private EndRoundWindow _endRoundWindow;
        
        [SerializeField] private List<WindowView> _windows;
        
        private PauseService _pauseService;

        [Inject]
        private void Constructor(PauseService pauseService)
        {
            _pauseService = pauseService;

            if (_windows.Count != 0)
            {
                SelectWindow(_windows[0]);
            }
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

        public void ShowWinningWindow()
        {
            SelectWindow(_endRoundWindow);
            _endRoundWindow.ShowWinningWindow();
        }
        
        public void ShowLoseWindow()
        {
            SelectWindow(_endRoundWindow);
            _endRoundWindow.ShowLoseWindow();
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
