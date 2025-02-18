using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts;
using UnityEngine;
using Zenject;

namespace Shot_Shift.UI.Scripts.StartScene
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private List<WindowView> _windows;

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            
            if(_windows.Count != 0)
            {
                SelectWindow(_windows[0]);
            }
        }

        public void SelectWindow(WindowView window)
        {
            foreach (WindowView windowView in _windows)
            {
                windowView.HideWindow();
            }
            
            window.ShowWindow();
        }

        public void OnStartSelected()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void OnExitSelected()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}