using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.Component.Enemy
{
    public struct Follow
    {
        public Transform target;
    }
    public struct AggroTimer
    {
        public float Cooldown;
    }
}
