using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Player;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly PauseService _pauseService;

        public void Enter()
        {
        }

        public void Exit()
        {
            _pauseService.CleanUp();
        }
    }
}