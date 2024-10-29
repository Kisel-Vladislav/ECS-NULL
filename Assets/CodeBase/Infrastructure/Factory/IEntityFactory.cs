using Leopotam.Ecs;

namespace CodeBase.Infrastructure.Factory
{
    public interface IEntityFactory
    {
        public EcsWorld World { set; }

        EcsEntity CreatePlayer();
        EcsEntity SetupWeapon(ref EcsEntity owner);
    }
}

