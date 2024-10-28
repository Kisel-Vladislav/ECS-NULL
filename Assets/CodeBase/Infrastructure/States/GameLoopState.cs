using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public void Enter()
        {
        }

        public Task Exit() =>
            Task.CompletedTask;
    }
}