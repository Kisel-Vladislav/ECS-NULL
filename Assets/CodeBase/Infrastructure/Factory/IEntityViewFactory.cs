using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEntityViewFactory
    {
        GameObject CreatePlayer(ref EcsEntity player, Vector3 at, Quaternion rotation);
        GameObject SetupWeapon(ref EcsEntity owner);
    }
}

