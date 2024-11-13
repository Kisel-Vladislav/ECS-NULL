using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelEditorState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelEditorState(IUIFactory uiFactory, SceneLoader sceneLoader)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _uiFactory.Root.ShowCurtain();
            _sceneLoader.Load(SceneName.LvelEditor, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiFactory.Root.HideCurtain();
        }

        public Task Exit() =>
            Task.CompletedTask;
    }
}