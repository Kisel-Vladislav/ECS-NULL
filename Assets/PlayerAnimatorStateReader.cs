using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

public class PlayerAnimatorStateReader : MonoBehaviour, IAnimationStateReader
{
    public EcsEntity entity;
    private readonly int _reloadStateHash = Animator.StringToHash("reload");

    public void EnteredState(int stateHash)
    {
    }
    public void ExitedState(int stateHash)
    {
        if(stateHash == _reloadStateHash)
            entity.Get<HasWeapon>().weapon.Get<ReloadingFinished>();
    }
}
public interface IAnimationStateReader
{
    void EnteredState(int stateHash);
    void ExitedState(int stateHash);
}
