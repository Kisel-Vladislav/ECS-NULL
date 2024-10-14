using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private WeaponSettings _weaponSettings;

        public void Init()
        {
            foreach (var enemyView in Object.FindObjectsOfType<AgentView>())
            {
                var enemyEntity = InitializeEnemyEntity(enemyView);
                InitializeWeaponForEnemy(ref enemyEntity, enemyView);
                enemyEntity.Get<TryAggro>();
            }
        }

        private EcsEntity InitializeEnemyEntity(AgentView enemyView)
        {
            var enemyEntity = _world.NewEntity();

            var aggro = enemyView.GetComponentInChildren<Aggro>();
            aggro.entity = enemyEntity;

            ref var enemy = ref enemyEntity.Get<AgentComponent>();
            ref var health = ref enemyEntity.Get<Health>();
            ref var animatorRef = ref enemyEntity.Get<AnimatorRef>();
            ref var transformRef = ref enemyEntity.Get<TransformRef>();

            enemyView.EntityView.Entity = enemyEntity;

            health.value = enemyView.startHealth;
            enemy.navMeshAgent = enemyView.navMeshAgent;
            transformRef.transform = enemyView.transform;
            animatorRef.animator = enemyView.animator;

            return enemyEntity;
        }

        private void InitializeWeaponForEnemy(ref EcsEntity enemyEntity, AgentView enemyView)
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
