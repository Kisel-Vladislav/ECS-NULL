using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelEditorState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public LoadLevelEditorState(IUIFactory uiFactory, SceneLoader sceneLoader, GameStateMachine stateMachine)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public async void Enter()
        {
            await _uiFactory.Root.ShowCurtain();
            _sceneLoader.Load(SceneName.LvelEditor, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiFactory.CreateLevelEditorHud();
            _uiFactory.Root.HideCurtain();

            _stateMachine.Enter<LevelEditorLoop>();
        }

        public Task Exit() =>
            Task.CompletedTask;
    }
}
