using CodeBase.ECS.Component;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using CodeBase.UI;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private StaticData configuration;
        private SceneData sceneData;
        private WeaponSettings WeaponSettings;
        private Hud Hud;
        private RuntimeData runtimeData;

        public void Init()
        {
            var playerEntity = CreatePlayerEntity();
            InitializePlayerComponents(playerEntity);
            SetupWeapon(playerEntity);
        }

        private EcsEntity CreatePlayerEntity()
        {
            EcsEntity playerEntity = _world.NewEntity();
            runtimeData.playerEntity = playerEntity;

            return playerEntity;
        }

        private void InitializePlayerComponents(EcsEntity playerEntity)
        {
            ref var player = ref playerEntity.Get<PlayerMove>();
            ref var transformRef = ref playerEntity.Get<TransformRef>();
            ref var health = ref playerEntity.Get<Health>();

            var playerGameObject = Object.Instantiate(configuration.PlayerPrefab, sceneData.PlayerSpawnPoint.position, Quaternion.identity);

            var entityView = playerGameObject.GetComponent<EntityView>();
            entityView.Entity = playerEntity;

            health.value = 100; // TO DO to static data
            transformRef.transform = playerGameObject.GetComponent<Transform>();

            player.playerSpeed = configuration.PlayerSpeed;
            player.CharacterController = playerGameObject.GetComponent<CharacterController>();

            InitializePlayerAnimator(playerEntity, playerGameObject);
        }

        private void InitializePlayerAnimator(EcsEntity playerEntity, GameObject playerGameObject)
        {
            var playerAnimatorStateReader = playerGameObject.GetComponent<PlayerAnimatorStateReader>();
            playerAnimatorStateReader.entity = playerEntity;

            ref var animatorRef = ref playerEntity.Get<AnimatorRef>();
            animatorRef.animator = playerGameObject.GetComponent<Animator>();
        }

        private void SetupWeapon(EcsEntity playerEntity)
        {
            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            var weaponEntity = _world.NewEntity();
            hasWeapon.weapon = weaponEntity;

            var playerGameObject = runtimeData.playerEntity.Get<TransformRef>().transform.gameObject;
            var weaponGameObject = Object.Instantiate(WeaponSettings.WeaponPrefab, playerGameObject.GetComponent<WeaponParent>().Pistol);
            var weaponView = weaponGameObject.GetComponent<WeaponView>();

            ref var weapon = ref weaponEntity.Get<Weapon>();
            weapon.owner = playerEntity;
            weapon.projectilePrefab = WeaponSettings.ProjectilePrefab;
            weapon.projectileRadius = WeaponSettings.ProjectileRadius;
            weapon.projectileSocket = weaponView.ProjectileSocket;
            weapon.projectileSpeed = WeaponSettings.ProjectileSpeed;
            weapon.totalAmmo = WeaponSettings.TotalAmmo;
            weapon.weaponDamage = WeaponSettings.WeaponDamage;
            weapon.currentInMagazine = WeaponSettings.CurrentInMagazine;
            weapon.maxInMagazine = WeaponSettings.MaxInMagazine;
            weapon.Cooldown = WeaponSettings.Cooldown;

            Hud.SetAmmo(weapon.currentInMagazine, weapon.maxInMagazine);
        }
    }
}
