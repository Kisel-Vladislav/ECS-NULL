using CodeBase.Infrastructure.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class ReplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public ReplayState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(SceneName.Dispose, OnLoad);
        private void OnLoad() => 
            _stateMachine.Enter<LoadLevelState>();

        public void Exit()
        {
        }
    }
}