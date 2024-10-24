using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class GameStateSwitchButton : MonoBehaviour
    {
        [SerializeField] private TargetState targetState;
        [SerializeField] private Button _button;

        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake() => _button.onClick.AddListener(OnClick);
        private void OnDestroy() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            switch (targetState)
            {
                case TargetState.LoadLevelState:
                    _gameStateMachine.Enter<LoadLevelState>(); break;
                case TargetState.LobbyState:
                    _gameStateMachine.Enter<LobbyState>(); break;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(targetState), targetState, "Unhandled target state.");
            }
        }
    }
}
