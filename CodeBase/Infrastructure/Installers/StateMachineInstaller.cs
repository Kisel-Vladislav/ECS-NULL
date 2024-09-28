using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindingStateMachine();
            BindingStateFactory();
            BindingStates();
        }
        private void BindingStateFactory()
        {
            Container.Bind<StateFactory>()
                     .AsSingle()
                     .NonLazy();
        }
        private void BindingStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>()
                     .AsSingle();
        }
        private void BindingStates()
        {
            Container.Bind<BootstrapState>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<LoadProgressState>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<LobbyState>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<LoadLevelState>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<GameLoopState>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}
