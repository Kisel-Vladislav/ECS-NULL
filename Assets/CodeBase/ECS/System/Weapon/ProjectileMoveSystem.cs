using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.WeaponSystem
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private const int LayerAggroZone = 6;
        private const int LayerMask = ~(1 << LayerAggroZone);

        private EcsFilter<Projectile> _filter;
        private Collider[] hitColliders = new Collider[1];

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);

                var initialPositionCheck = Physics.CheckSphere(projectile.projectileGO.transform.position, projectile.radius, LayerMask);
                if (initialPositionCheck)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();

                    Physics.OverlapSphereNonAlloc(projectile.projectileGO.transform.position, projectile.radius, hitColliders, LayerMask);
                    if (hitColliders.Length > 0)
                        projectileHit.HitGameObject = hitColliders[0].gameObject;
                    continue;
                }

                var position = projectile.projectileGO.transform.position;
                position += projectile.direction * projectile.speed * Time.deltaTime;
                projectile.projectileGO.transform.position = position;


                var displacementSinceLastFrame = position - projectile.previousPos;
                var hit = Physics.SphereCast(projectile.previousPos, projectile.radius,
                displacementSinceLastFrame.normalized, out var hitInfo, displacementSinceLastFrame.magnitude, LayerMask);

                if (hit)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();
                    projectileHit.HitGameObject = hitInfo.collider.gameObject;
                }

                projectile.previousPos = projectile.projectileGO.transform.position;
            }
        }
    }
}
