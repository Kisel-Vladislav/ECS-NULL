using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UI;
using CodeBase.UI.Window;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public UIRoot Root { get; private set; }
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, GameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
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
        public void CreateLevelEditorHud()
        {
            var hud = _assetProvider.Instance<LevelEditorHud>(AssetsPath.LevelEditorHud);
            hud.BuildPanelContentController.Construct(new BuildItemViewFactory(_assetProvider), _staticDataService);
            Root.AddWindow(hud.transform);
        }
        public void Clear()
        {
            Root.Clear();
        }
        public Task<FailWindow.FailWindowResult> ShowFailWindow()
        {
            var failWindow = _assetProvider.Instance<FailWindow>(AssetsPath.FailWindow);
            Root.AddWindow(failWindow.transform);
            return failWindow.InitAndShow();
        }
        public Task<WinWindow.WinWindowResult> ShowWinWindow()
        {
            var winWindow = _assetProvider.Instance<WinWindow>(AssetsPath.WinWindow);
            Root.AddWindow(winWindow.transform);
            return winWindow.InitAndShow();
        }

    }
}