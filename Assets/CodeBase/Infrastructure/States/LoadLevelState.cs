using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.Level;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private IPlayerFactory _playerFactory;
        private ILevelService _levelService;
        private IEntityFactory _entityFactory;
        private IEntityViewFactory _entityViewFactory;
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ILevelService levelService, IPlayerFactory playerFactory, IUIFactory uiFactory, IEntityFactory entityFactory, IEntityViewFactory entityViewFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _levelService = levelService;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
            _entityFactory = entityFactory;
            _entityViewFactory = entityViewFactory;
        }

        public async void Enter()
        {
            await _uiFactory.Root.ShowCurtain();
            _sceneLoader.Load(SceneName.Game, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            //_uiFactory.CreateHud();
            _stateMachine.Enter<GameLoopState>();
        }
        private void InitGameWorld()
        {
            //var player =  _entityFactory.CreatePlayer();
            //_entityViewFactory.CreatePlayer(player,_levelService.WordObjectCollector.SpawnPoint.position,Quaternion.identity);

            //var weapon = _entityFactory.SetupWeapon(player);
            //_entityViewFactory.SetupWeapon(weapon);
            //_playerFactory.Create(_levelService.WordObjectCollector.SpawnPoint.position,Quaternion.identity);
        }
        public async Task Exit()
        {
            await _uiFactory.Root.HideCurtain();
        }
    }
}