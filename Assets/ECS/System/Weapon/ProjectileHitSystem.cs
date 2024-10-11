using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;

namespace CodeBase.ECS.WeaponSystem
{
    public class ProjectileHitSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile, ProjectileHit> _filter;
        private EcsWorld _world;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);
                ref var hit = ref _filter.Get2(i);

                if (hit.raycastHit.collider.gameObject.TryGetComponent(out EntityView entityView))
                {
                    if (entityView.Entity.IsAlive())
                    {
                        ref var e = ref _world.NewEntity().Get<DamageEvent>();
                        e.Target = entityView.Entity;
                        e.Value = projectile.damage;
                    }
                }

                projectile.projectileGO.SetActive(false);
                _filter.GetEntity(i).Destroy();
            }
        }
    }
}
