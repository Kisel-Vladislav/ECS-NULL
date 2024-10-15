using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

public class AgentAnimatorStateReader : MonoBehaviour, IAnimationStateReader
{
    public EcsEntity entity;
    private readonly int _reloadStateHash = Animator.StringToHash("reload");
    private readonly int _dodgeStateHash = Animator.StringToHash("dodge");

    public void EnteredState(int stateHash)
    {
    }
    public void ExitedState(int stateHash)
    {
        if(stateHash == _reloadStateHash)
            entity.Get<HasWeapon>().weapon.Get<ReloadingFinished>();
        if (stateHash == _dodgeStateHash)
            entity.Get<DodgingFinished>();
    }
}
public interface IAnimationStateReader
{
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
}
