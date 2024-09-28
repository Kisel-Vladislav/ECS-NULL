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
        private IPlayerFactory _playerFactory;
        private ILevelService _levelService;
        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ILevelService levelService, IPlayerFactory playerFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _levelService = levelService;
            _playerFactory = playerFactory;
        }

        public void Enter()
        {
            _sceneLoader.Load(SceneName.Game, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }
        private void InitGameWorld()
        {
            _playerFactory.Create(_levelService.WordObjectCollector.SpawnPoint.position,Quaternion.identity);
        }
        public void Exit()
        {

        }
    }
}