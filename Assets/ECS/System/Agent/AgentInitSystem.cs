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
        private SceneData _sceneData;

        public void Init()
        {
            foreach (var enemyView in _sceneData.Enemy)
            {
                var enemyEntity = InitializeAgentEntity(enemyView, TeamType.Enemy);
                InitializeWeaponForAgent(ref enemyEntity, enemyView);
                enemyEntity.Get<CheckDetectionZone>();
            }

            foreach (var allyView in _sceneData.Ally)
            {
                var allyEntity = InitializeAgentEntity(allyView, TeamType.Ally);
                InitializeWeaponForAgent(ref allyEntity, allyView);
                allyEntity.Get<CheckDetectionZone>();
            }
        }

        private EcsEntity InitializeAgentEntity(AgentView agentView, TeamType teamType)
        {
            var agentEntity = _world.NewEntity();

            var aggro = agentView.GetComponentInChildren<Aggro>();
            aggro.entity = agentEntity;

            ref var agent = ref agentEntity.Get<AgentComponent>();
            ref var health = ref agentEntity.Get<Health>();
            ref var animatorRef = ref agentEntity.Get<AnimatorRef>();
            ref var transformRef = ref agentEntity.Get<TransformRef>();
            ref var team = ref agentEntity.Get<TeamComponent>();

            team.Team = teamType;

            agentView.EntityView.Entity = agentEntity;

            health.value = agentView.startHealth;
            agent.navMeshAgent = agentView.navMeshAgent;
            transformRef.transform = agentView.transform;

            var playerAnimatorStateReader = agentView.GetComponent<AgentAnimatorStateReader>();
            playerAnimatorStateReader.entity = agentEntity;

            animatorRef.animator = agentView.animator;

            return agentEntity;
        }

        private void InitializeWeaponForAgent(ref EcsEntity agentEntity, AgentView agentView)
        {
            ref var hasWeapon = ref agentEntity.Get<HasWeapon>();
            var weaponEntity = _world.NewEntity();
            hasWeapon.weapon = weaponEntity;

            var weaponGameObject = Object.Instantiate(_weaponSettings.WeaponPrefab, agentView.GetComponent<WeaponParent>().Pistol);
            var weaponView = weaponGameObject.GetComponent<WeaponView>();

            ref var weapon = ref weaponEntity.Get<Weapon>();
            weapon.owner = agentEntity;
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
