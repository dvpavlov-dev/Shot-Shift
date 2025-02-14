namespace Shot_Shift.Infrastructure.Scripts
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
