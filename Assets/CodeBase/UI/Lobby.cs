using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI
{
    public class Lobby : MonoBehaviour
    {
        [SerializeField] GameStateSwitchButton PlayButton;

        public void Construct(GameStateMachine gameStateMachine)
        {
            PlayButton.Construct(gameStateMachine);
        }
    }
}
