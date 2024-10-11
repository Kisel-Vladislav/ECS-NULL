using UnityEngine;

namespace CodeBase.ECS.Component.Agent
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
