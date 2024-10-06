using Leopotam.Ecs;

namespace CodeBase.ECS.Component.Enemy
{
    public struct Follow
    {
        public EcsEntity target;
        public float nextAttackTime;
    }
}
