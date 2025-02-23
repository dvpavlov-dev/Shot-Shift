using Shot_Shift.Infrastructure.Scripts;
using Shot_Shift.Infrastructure.Scripts.States;
using Shot_Shift.UI.Scripts.StartScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class EndRoundWindow : WindowView
    {
        [SerializeField] private Image _backgroundTitleImage;
        [SerializeField] private TMP_Text _titleText;

        private GameStateMachine _gameStateMachine;
        private int _currentLevel;

        [Inject]
        private void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void ShowWinningWindow()
        {
            _backgroundTitleImage.color = Color.green;
            _titleText.text = "You Win!";
        }

        public void ShowLoseWindow()
        {
            _backgroundTitleImage.color = Color.red;
            _titleText.text = "You Lose!";
        }

        public void OnContinueSelected()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
        
        public void ReturnToStartScene()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
    }
}
