using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class ReplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IUIFactory _uIFactory;
        private readonly SceneLoader _sceneLoader;

        public ReplayState(GameStateMachine stateMachine,IUIFactory uIFactory, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _uIFactory = uIFactory;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _uIFactory.Root.ShowCurtain();
            _sceneLoader.Load(SceneName.Dispose, OnLoad);
        }

        private void OnLoad() => 
            _stateMachine.Enter<LoadLevelState>();

        public Task Exit() =>
            Task.CompletedTask;
    }
}