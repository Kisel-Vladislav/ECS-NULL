using CodeBase.Player.Data;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly PersistentProgress _progress;

        public LoadProgressState(GameStateMachine stateMachine, PersistentProgress progress)
        {
            _stateMachine = stateMachine;
            _progress = progress;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LobbyState>();
        }

        private void LoadProgressOrInitNew()
        {
            _progress.Player = Load() ?? new PlayerData();
        }

        private PlayerData Load()
        {
            return null;
        }

        public void Exit()
        {
        }
    }
}