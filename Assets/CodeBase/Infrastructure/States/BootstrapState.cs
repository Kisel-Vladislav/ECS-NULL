using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IAudioService _audioService;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IStaticDataService staticDataService, IAudioService audioService, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _audioService = audioService;
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            InitializeServices();
            _uiFactory.CreateUIRoot();
            _stateMachine.Enter<LoadProgressState>();
        }
        public void Exit()
        {
        }

        private void InitializeServices()
        {
            InitStaticData();
            _audioService.Init();
        }
        private void InitStaticData()
        {
        }

    }
}