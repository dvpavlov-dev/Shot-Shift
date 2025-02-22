using Shot_Shift.Infrastructure.Scripts;
using Shot_Shift.UI.Scripts.StartScene;
using Zenject;

namespace Shot_Shift.UI.Scripts.GameLoopScene
{
    public class MenuWindow : WindowView
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Constructor(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void ReturnToStartScene()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
    }
}
