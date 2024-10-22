using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameStateMachine _gameStateMachine;

        public UIRoot Root { get; private set; }
        public UIFactory(IAssetProvider assetProvider, GameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void CreateUIRoot()
        {
            Root = _assetProvider.Instance<UIRoot>(AssetsPath.UIRoot);
            Root.Init();
        }
        public void CreateLobby()
        {
            var lobby = _assetProvider.Instance<Lobby>(AssetsPath.Lobby);
            lobby.Construct(_gameStateMachine);
            Root.AddWindow(lobby.transform);
        }
        public Hud CreateHud()
        {
            var hud = _assetProvider.Instance<Hud>(AssetsPath.Hud);
            Root.AddWindow(hud.transform);
            return hud;
        }

        public void Clear()
        {
            Root.Clear();
        }
    }
}