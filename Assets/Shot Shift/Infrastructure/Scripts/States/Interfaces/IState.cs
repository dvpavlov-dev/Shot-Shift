namespace Shot_Shift.Infrastructure.Scripts
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
