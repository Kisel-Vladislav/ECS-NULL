using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.WeaponSystem
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile> _filter;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);

                var position = projectile.projectileGO.transform.position;
                position += projectile.direction * projectile.speed * Time.deltaTime;
                projectile.projectileGO.transform.position = position;

                var displacementSinceLastFrame = position - projectile.previousPos;
                var hit = Physics.SphereCast(projectile.previousPos, projectile.radius,
               displacementSinceLastFrame.normalized, out var hitInfo, displacementSinceLastFrame.magnitude);

                if (hit)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();
                    projectileHit.raycastHit = hitInfo;
                }

                projectile.previousPos = projectile.projectileGO.transform.position;
            }
        }
    }
}
