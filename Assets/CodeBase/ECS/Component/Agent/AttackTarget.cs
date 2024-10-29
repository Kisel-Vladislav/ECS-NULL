using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.Component.Agent
{
    public struct AttackTarget
    {
        public Transform Target;
        public EcsEntity Entity;
    }
}
