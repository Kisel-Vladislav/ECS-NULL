using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEntityViewFactory
    {
        GameObject CreateAgentView(ref EcsEntity agent, AgentView agentView);
        GameObject CreatePlayer(ref EcsEntity player, Vector3 at, Quaternion rotation);
        GameObject SetupWeapon(ref EcsEntity owner);
    }
}

