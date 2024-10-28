using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class LobbyState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;

        public LobbyState(IUIFactory uiFactory, SceneLoader sceneLoader)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _uiFactory.Root.ShowCurtain();
            _sceneLoader.Load(SceneName.Lobby, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiFactory.CreateLobby();
            _uiFactory.Root.HideCurtain();
        }

        public async Task Exit()
        {
            await _uiFactory.Root.ShowCurtain();
            _uiFactory.Clear();
        }
    }
}