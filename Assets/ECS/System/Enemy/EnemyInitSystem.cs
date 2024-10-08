using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Enemy;
using CodeBase.ECS.Data;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Enemy
{
    public class EnemyAggroSystem : IEcsRunSystem
    {
        private const float AggroCooldown = 3f; //TO DO to static data

        private EcsFilter<EnterAggro> _enterFilter;
        private EcsFilter<ExitAggro> _exitFilter;
        public void Run()
        {
            AggroEnter();
            AggroExit();
        }

        private void AggroExit()
        {
            foreach (var i in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(i);
                ref var timer = ref entity.Get<AggroTimer>();
                timer.Cooldown = AggroCooldown;
            }
        }
        private void AggroEnter()
        {
            foreach (var i in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(i);
                ref var aggroTarget = ref _enterFilter.Get1(i);

                ref var follow = ref entity.Get<Follow>();
                follow.target = aggroTarget.target;
            }
        }
    }
    public class AggroTimerSystem : IEcsRunSystem
    {
        private EcsFilter<AggroTimer> _filter;
        public void Run()
        {
            UpdateAggroTimers();
        }

        private void UpdateAggroTimers()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                timer.Cooldown -= Time.deltaTime;

                if (timer.Cooldown <= 0)
                    RemoveAggroTimer(i);
            }
        }
        private void RemoveAggroTimer(int i)
        {
            ref var entity = ref _filter.GetEntity(i);
            entity.Del<AggroTimer>();
        }
    }
    public class EnemyFollowSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, Follow, AnimatorRef> followingEnemies;

        public void Run()
        {
            foreach (var i in followingEnemies)
            {
                ref var enemy = ref followingEnemies.Get1(i);
                ref var follow = ref followingEnemies.Get2(i);

                var targetPos = follow.target.position;

                enemy.navMeshAgent.SetDestination(targetPos);
            }
        }
    }
    public class AgentInputSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var agent = ref _filter.Get1(i);

                ref var input = ref entity.Get<MoveInput>();
                input.vector = agent.navMeshAgent.desiredVelocity;
            }
        }
    }
    public class AgentAttackSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, Follow, TransformRef, HasWeapon> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var follow = ref _filter.Get2(i);
                ref var transform = ref _filter.Get3(i);
                ref var hasWeapon = ref _filter.Get4(i);

                var ray = new Ray(transform.transform.position, follow.target.position.normalized);

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
    public class LookAtSystem : IEcsRunSystem
    {
        private EcsFilter<LookAt, TransformRef> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var at = ref _filter.Get1(i);
                ref var transform = ref _filter.Get2(i);

                transform.transform.LookAt(at.transform);
            }
        }
    }
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WeaponSettings _weaponSettings;

        public void Init()
        {
            foreach (var enemyView in Object.FindObjectsOfType<EnemyView>())
            {
                var enemyEntity = InitializeEnemyEntity(enemyView);
                InitializeWeaponForEnemy(ref enemyEntity, enemyView);
            }
        }

        private EcsEntity InitializeEnemyEntity(EnemyView enemyView)
        {
            var enemyEntity = _world.NewEntity();

            var aggro = enemyView.GetComponentInChildren<Aggro>();
            aggro.entity = enemyEntity;

            ref var enemy = ref enemyEntity.Get<EnemyComponent>();
            ref var health = ref enemyEntity.Get<Health>();
            ref var animatorRef = ref enemyEntity.Get<AnimatorRef>();
            ref var transformRef = ref enemyEntity.Get<TransformRef>();

            enemyEntity.Get<Idle>();
            enemyView.entity = enemyEntity;

            health.value = enemyView.startHealth;
            enemy.damage = enemyView.damage;
            enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
            enemy.navMeshAgent = enemyView.navMeshAgent;

            enemy.transform = enemyView.transform;
            transformRef.transform = enemyView.transform;

            enemy.meleeAttackInterval = enemyView.meleeAttackInterval;
            enemy.triggerDistance = enemyView.triggerDistance;
            animatorRef.animator = enemyView.animator;

            return enemyEntity;
        }

        private void InitializeWeaponForEnemy(ref EcsEntity enemyEntity, EnemyView enemyView)
        {
            ref var hasWeapon = ref enemyEntity.Get<HasWeapon>();
            var weaponEntity = _world.NewEntity();
            hasWeapon.weapon = weaponEntity;

            var weaponGameObject = Object.Instantiate(_weaponSettings.WeaponPrefab, enemyView.GetComponent<WeaponParent>().Pistol);
            var weaponView = weaponGameObject.GetComponent<WeaponView>();

            ref var weapon = ref weaponEntity.Get<Weapon>();
            weapon.owner = enemyEntity;
            weapon.projectilePrefab = _weaponSettings.ProjectilePrefab;
            weapon.projectileRadius = _weaponSettings.ProjectileRadius;
            weapon.projectileSocket = weaponView.ProjectileSocket;
            weapon.projectileSpeed = _weaponSettings.ProjectileSpeed;
            weapon.totalAmmo = _weaponSettings.TotalAmmo;
            weapon.weaponDamage = _weaponSettings.WeaponDamage;
            weapon.currentInMagazine = _weaponSettings.CurrentInMagazine;
            weapon.maxInMagazine = _weaponSettings.MaxInMagazine;
            weapon.Cooldown = _weaponSettings.Cooldown;
        }
    }
}
