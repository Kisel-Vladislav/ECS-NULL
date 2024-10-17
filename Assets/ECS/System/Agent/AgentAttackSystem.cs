using Assets;
using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using System.Text;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentAttackSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, AttackTarget, TransformRef, HasWeapon> _attackFilter;
        private EcsFilter<AgentComponent, StopAttack> _stopAttackFilter;

        public void Run()
        {
            StopAttack();
            Attack();
        }

        private void Attack()
        {
            foreach (var i in _attackFilter)
            {
                ref var entity = ref _attackFilter.GetEntity(i);
                ref var attackTarget = ref _attackFilter.Get2(i);
                ref var transform = ref _attackFilter.Get3(i);
                ref var hasWeapon = ref _attackFilter.Get4(i);

                var ray = new Ray(transform.transform.position.AddY(1f), (attackTarget.Target.position - transform.transform.position).normalized);
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

                if (!attackTarget.Entity.IsAlive())
                {
                    StopAimingAndClearTarget(ref entity);
                    continue;
                }

                if (Physics.Raycast(ray,out var hitInfo, 100f) && hitInfo.collider.gameObject == attackTarget.Target.gameObject) // TO DO Weapon.EffectiveDistance
                        TryShoot(ref entity, ref attackTarget, ref hasWeapon);
                else
                    StopAiming(ref entity);
            }
        }

        private void StopAttack()
        {
            foreach (var i in _stopAttackFilter)
            {
                ref var entity = ref _stopAttackFilter.GetEntity(i);
                StopAimingAndClearTarget(ref entity);
                entity.Del<StopAttack>();
            }
        }
        private void StopAimingAndClearTarget(ref EcsEntity entity)
        {
            StopAiming(ref entity);
            entity.Del<AttackTarget>();
        }
        private void StopAiming(ref EcsEntity entity)
        {
            entity.Get<AimFinished>();

            entity.Del<LookAt>();
        }
        private void TryShoot(ref EcsEntity entity, ref AttackTarget attackTarget, ref HasWeapon hasWeapon)
        {
            ref var weapon = ref hasWeapon.weapon.Get<Weapon>();
            if(weapon.currentInMagazine > 0)
            { 
                entity.Get<TryAim>();
                ref var lookAt = ref entity.Get<LookAt>();
                lookAt.transform = attackTarget.Target.transform;
                hasWeapon.weapon.Get<Shoot>();
            }
            else
                entity.Get<TryReload>();
        }
    }
}
