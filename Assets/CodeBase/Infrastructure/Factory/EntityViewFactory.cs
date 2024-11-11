using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.System;
using CodeBase.ECS.WeaponComponent;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StaticData;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class EntityViewFactory : IEntityViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public EntityViewFactory(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public GameObject CreatePlayer(ref EcsEntity player, Vector3 at, Quaternion rotation)
        {
            var playerStaticData = _staticDataService.ForPlayer();
            var playerView = _assetProvider.Instance(playerStaticData.Prefab, at, rotation);

            ref var transformRef = ref player.Get<TransformRef>();
            transformRef.transform = playerView.transform;

            ref var characterController = ref player.Get<CharacterControllerComponent>();
            characterController.CharacterController = playerView.GetComponent<CharacterController>();

            ref var weaponAttachmentPoint = ref player.Get<WeaponAttachmentPoint>();
            weaponAttachmentPoint.attachmentTransform = playerView.GetComponent<WeaponParent>().Pistol;

            var entityView = playerView.GetComponent<EntityView>();
            entityView.Entity = player;


            InitializeAnimator(ref player, playerView);

            return playerView;
        }
        private void InitializeAnimator(ref EcsEntity player, GameObject playerGameObject)
        {
            var playerAnimatorStateReader = playerGameObject.GetComponent<AgentAnimatorStateReader>();
            playerAnimatorStateReader.entity = player;

            ref var animatorRef = ref player.Get<AnimatorRef>();
            animatorRef.animator = playerGameObject.GetComponent<Animator>();
        }
        public GameObject SetupWeapon(ref EcsEntity owner)
        {
            ref var attachmentPoint = ref owner.Get< WeaponAttachmentPoint>();
            ref var hasWeapon = ref owner.Get<HasWeapon>();
            var attachmentTransform = attachmentPoint.attachmentTransform;

            var weaponStaticData = _staticDataService.ForWeapon();
            var weaponView = Object.Instantiate(weaponStaticData.WeaponPrefab, attachmentTransform);

            //weaponView.transform.SetParent(attachmentTransform, false);

            var weaponViewComponent = weaponView.GetComponent<WeaponView>();
            ref var weaponData = ref hasWeapon.weapon.Get<Weapon>();
            weaponData.projectileSocket = weaponViewComponent.ProjectileSocket;

            return weaponView;
        }

        public GameObject CreateAgentView(ref EcsEntity agent, AgentView agentView)
        {
            ref var transformRef = ref agent.Get<TransformRef>();
            transformRef.transform = agentView.transform;

            ref var characterController = ref agent.Get<CharacterControllerComponent>();
            characterController.CharacterController = agentView.CharacterController;

            ref var weaponAttachmentPoint = ref agent.Get<WeaponAttachmentPoint>();
            weaponAttachmentPoint.attachmentTransform = agentView.GetComponent<WeaponParent>().Pistol;

            ref var animatorRef = ref agent.Get<AnimatorRef>();
            animatorRef.animator = agentView.animator;

            var entityView = agentView.GetComponent<EntityView>();
            entityView.Entity = agent;

            ref var agentComponent = ref agent.Get<AgentComponent>();
            agentComponent.navMeshAgent = agentView.navMeshAgent;

            var aggro = agentView.GetComponentInChildren<DetectionZone>();
            aggro.entity = agent;

            InitializeAnimator(ref agent, agentView.gameObject);

            return agentView.gameObject;
        }
    }
}

