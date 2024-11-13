using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI
{
    public class Lobby : MonoBehaviour
    {
        [SerializeField] GameStateSwitchButton PlayButton;
        [SerializeField] GameStateSwitchButton LevelEditorButton;

        public void Construct(GameStateMachine gameStateMachine)
        {
            PlayButton.Construct(gameStateMachine);
            LevelEditorButton.Construct(gameStateMachine);
        }
    }
}
