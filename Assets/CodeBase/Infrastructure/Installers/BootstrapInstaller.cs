using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.Service.InputService;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Level;
using CodeBase.Infrastructure.Services.Player;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Player.Data;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindingAssetManagementService();
            BindingCoreService();
            BindingDataService();
            BindingPlayerService();
        }

        private void BindingDataService()
        {
            BindingPersistentProgress();
        }
        private void BindingCoreService()
        {
            BindingCoroutineRunner();
            BindingPauseService();
            BindingLevelService();
            BindingAudioService();
        }
        private void BindingAssetManagementService()
        {
            BindingSceneLoader();
            BindingAssetProvider();
            BindingStataDataService();
        }
        private void BindingPlayerService()
        {
            BindingPlayerProvider();
            BindingInputService();
        }
        private void BindFactories()
        {
            BindingPlayerFactory();
            BindingUIFactory();
            BindingEntityFactory();
            BindingEntityViewFactory();
        }
        private void BindingPersistentProgress() =>
            Container.Bind<PersistentProgress>()
                .AsSingle()
                .NonLazy();
        private void BindingLevelService() =>
            Container.Bind<ILevelService>()
                .To<LevelService>()
                .AsSingle()
                .NonLazy();
        private void BindingEntityViewFactory() =>
            Container.Bind<IEntityViewFactory>()
                .To<EntityViewFactory>()
                .AsSingle()
                .NonLazy();
        private void BindingEntityFactory() =>
            Container.Bind<IEntityFactory>()
                .To<EntityFactory>()
                .AsSingle()
                .NonLazy();
        private void BindingUIFactory() =>
            Container.Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle()
                .NonLazy();
        private void BindingPlayerFactory() =>
            Container.Bind<IPlayerFactory>()
                .To<PlayerFactory>()
                .AsSingle()
                .NonLazy();
        private void BindingPauseService() =>
            Container.Bind<PauseService>()
                .AsSingle();
        private void BindingSceneLoader() =>
            Container.Bind<SceneLoader>()
                     .AsSingle()
                     .NonLazy();
        private void BindingPlayerProvider() =>
            Container.Bind<IPlayerProvider>()
                     .To<PlayerProvider>()
                     .AsSingle()
                     .NonLazy();
        private void BindingCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>()
                     .FromInstance(this)
                     .AsSingle();
        private void BindingAudioService() =>
            Container.Bind<IAudioService>()
                     .To<AudioService>()
                     .AsSingle()
                     .NonLazy();
        private void BindingInputService() =>
            Container.Bind<IInputService>()
                     .To<StandartInputService>()
                     .AsSingle()
                     .NonLazy();
        private void BindingStataDataService() =>
            Container.Bind<IStaticDataService>()
                     .To<StaticDataService>()
                     .AsSingle()
                     .NonLazy();
        private void BindingAssetProvider() =>
            Container.Bind<IAssetProvider>()
                     .To<AssetProvider>()
                     .AsSingle()
                     .NonLazy();
    }
}
