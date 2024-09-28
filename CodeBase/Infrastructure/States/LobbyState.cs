using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;

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

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Lobby, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiFactory.CreateLobby();
        }

        public void Exit()
        {
            _uiFactory.Clear();
        }
    }
}