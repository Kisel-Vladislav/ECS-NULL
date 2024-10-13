using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

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
                ref var projectileEntity = ref _filter.GetEntity(i);
                ref var projectile = ref _filter.Get1(i);
                ref var hit = ref _filter.Get2(i);

                var hitObject = hit.HitGameObject;
                if (TryHandleDamage(hitObject, projectile.damage))
                {
                    DisableProjectile(ref _filter.GetEntity(i), projectile);
                }
                projectileEntity.Destroy();
            }   
        }
        private bool TryHandleDamage(GameObject hitObject, int damage)
        {
            if (hitObject.TryGetComponent<EntityView>(out var entityView))
            {
                if (entityView.Entity.IsAlive())
                {
                    ref var damageEvent = ref _world.NewEntity().Get<DamageEvent>();
                    damageEvent.Target = entityView.Entity;
                    damageEvent.Value = damage;
                    return true;
                }
            }
            return false;
        }

        private void DisableProjectile(ref EcsEntity projectileEntity, Projectile projectile)
        {
            projectile.projectileGO.SetActive(false);
        }
    }
}
