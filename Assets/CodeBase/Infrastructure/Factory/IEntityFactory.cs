using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEntityFactory
    {
        public EcsWorld World { set; }

        EcsEntity CreateAgent(TeamType enemy);
        EcsEntity CreatePlayer();
        EcsEntity SetupWeapon(ref EcsEntity owner);
    }
}

