using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopWinState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopWinState(IUIFactory uiFactory, GameStateMachine gameStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            var playerChoise = await _uiFactory.ShowWinWindow();

            switch (playerChoise)
            {
                case UI.Window.WinWindow.WinWindowResult.Replay:
                    _gameStateMachine.Enter<ReplayState>();
                    break;
                case UI.Window.WinWindow.WinWindowResult.Lobby:
                    _gameStateMachine.Enter<LobbyState>();
                    break;
            }
        }
        public void Exit()
        {
        }
    }
}