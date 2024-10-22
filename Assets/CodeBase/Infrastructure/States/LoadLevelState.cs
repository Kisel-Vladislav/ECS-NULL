using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.Level;
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
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ILevelService levelService, IPlayerFactory playerFactory, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _levelService = levelService;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _uiFactory.Root.ShowCurtain();
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
            //_playerFactory.Create(_levelService.WordObjectCollector.SpawnPoint.position,Quaternion.identity);
        }
        public void Exit()
        {
            _uiFactory.Root.HideCurtain();
        }
    }
}