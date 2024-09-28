namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    public interface IExitableState
    {
        void Exit();
    }
    public interface IPayloadedState<Tpayload> : IExitableState
    {
        void Enter(Tpayload payload);
    }
}
