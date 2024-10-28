using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    public interface IExitableState
    {
        Task Exit();
    }
    public interface IPayloadedState<Tpayload> : IExitableState
    {
        void Enter(Tpayload payload);
    }
}
