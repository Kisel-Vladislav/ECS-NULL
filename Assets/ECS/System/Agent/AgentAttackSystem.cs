using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentAttackSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, Follow, TransformRef, HasWeapon> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var follow = ref _filter.Get2(i);
                ref var transform = ref _filter.Get3(i);
                ref var hasWeapon = ref _filter.Get4(i);

                var ray = new Ray(transform.transform.position, (follow.target.position - transform.transform.position).normalized);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

                if (Physics.Raycast(ray, 100)) // TO DO Weapon.EffectiveDistance
                    StartAimingAndShoot(ref entity, ref follow, ref hasWeapon);
                else
                    StopAiming(ref entity);

            }
        }

        private static void StopAiming(ref EcsEntity entity)
        {
            entity.Get<AimFinished>();
            entity.Del<LookAt>();
        }

        private static void StartAimingAndShoot(ref EcsEntity entity, ref Follow follow, ref HasWeapon hasWeapon)
        {
            entity.Get<TryAim>();
            ref var lookAt = ref entity.Get<LookAt>();
            lookAt.transform = follow.target.transform;
            hasWeapon.weapon.Get<Shoot>();
        }
    }
}
