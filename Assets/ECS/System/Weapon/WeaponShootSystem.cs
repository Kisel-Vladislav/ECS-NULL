using CodeBase.ECS.WeaponComponent;
using CodeBase.UI;
using Leopotam.Ecs;

namespace CodeBase.ECS.WeaponSystem
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private EcsFilter<Weapon, Shoot>
                .Exclude<BlockShootDuration> _filter;
        private Hud Hud;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                ref var entity = ref _filter.GetEntity(i);
                entity.Del<Shoot>();

                if (weapon.currentInMagazine <= 0)
                    continue;

                weapon.currentInMagazine--;
                ref var spawnProjectile = ref entity.Get<SpawnProjectile>();

                ref var block = ref entity.Get<BlockShootDuration>();
                block.Timer = weapon.Cooldown;
                Hud.SetAmmo(weapon.currentInMagazine, weapon.totalAmmo);

            }
        }
    }
}
