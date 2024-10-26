using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

public class WinTriger : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
	{
        _gameStateMachine = gameStateMachine;
	}

    private void OnTriggerEnter(Collider other)
    {
        _gameStateMachine.Enter<GameLoopWinState>();
    }

}
