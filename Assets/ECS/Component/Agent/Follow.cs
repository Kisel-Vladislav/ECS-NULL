using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.Component.Agent
{
    public struct Follow
    {
        public Transform Target;
        public EcsEntity Entity;
    }
    public struct AggroTimer
    {
        public float Cooldown;
    }
}
