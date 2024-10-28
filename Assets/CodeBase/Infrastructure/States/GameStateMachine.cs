using CodeBase.Infrastructure.Factory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        private readonly StateFactory _stateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public async void Enter<TState>() where TState : class, IState
        {
            var state = await ChangeState<TState>();
            state.Enter();
        }
        public async void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = await ChangeState<TState>();
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
        private async Task<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if(_activeState != null)
                await _activeState.Exit();

            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = _stateFactory.CreateState<BootstrapState>(),
                [typeof(LoadProgressState)] = _stateFactory.CreateState<LoadProgressState>(),
                [typeof(LobbyState)] = _stateFactory.CreateState<LobbyState>(),
                [typeof(LoadLevelState)] = _stateFactory.CreateState<LoadLevelState>(),
                [typeof(GameLoopState)] = _stateFactory.CreateState<GameLoopState>(),
                [typeof(GameLoopFailState)] = _stateFactory.CreateState<GameLoopFailState>(),
                [typeof(GameLoopWinState)] = _stateFactory.CreateState<GameLoopWinState>(),
                [typeof(ReplayState)] = _stateFactory.CreateState<ReplayState>(),
            };
            Enter<BootstrapState>();
        }
    }
}
