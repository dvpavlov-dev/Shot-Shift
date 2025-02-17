namespace Shot_Shift.Infrastructure.Scripts
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
        
        public void Exit()
        {
        }
    }
}