using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class Lobby : MonoBehaviour
    {
        [SerializeField] Button PlayButton;

        private GameStateMachine _gameStateMachine;

        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            PlayButton.onClick.AddListener(
                () => gameStateMachine.Enter<LoadLevelState>());
        }
    }
}
