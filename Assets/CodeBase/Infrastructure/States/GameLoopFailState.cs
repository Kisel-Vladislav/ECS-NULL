using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopFailState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopFailState(IUIFactory uiFactory, GameStateMachine gameStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            var playerChoise = await _uiFactory.ShowFailWindow();

            switch (playerChoise)
            {
                case UI.Window.FailWindow.FailWindowResult.Replay:
                    _gameStateMachine.Enter<ReplayState>();
                    break;
                case UI.Window.FailWindow.FailWindowResult.Lobby:
                    _gameStateMachine.Enter<LobbyState>();
                    break;
            }
        }
        public void Exit()
        {
        }
    }
}