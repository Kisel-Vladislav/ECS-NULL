using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.UI;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameStateMachine _gameStateMachine;

        public UIFactory(IAssetProvider assetProvider, GameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
        }

        private UIRoot _uiRoot;

        public void CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instance<UIRoot>(AssetsPath.UIRoot);
            _uiRoot.Init();
        }
        public void CreateLobby()
        {
            var lobby = _assetProvider.Instance<Lobby>(AssetsPath.Lobby);
            lobby.Construct(_gameStateMachine);
            _uiRoot.AddWindow(lobby.transform);
        }

        public void Clear()
        {
            _uiRoot.Clear();
        }
    }
}