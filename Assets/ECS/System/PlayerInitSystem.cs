using CodeBase.ECS.Component;
using CodeBase.ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.System
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private StaticData configuration;
        private SceneData sceneData;
        private WeaponSettings WeaponSettings;
        public void Init()
        {
            EcsEntity playerEntity = _world.NewEntity();

            ref var player = ref playerEntity.Get<Component.Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();

            var playerGameObject = Object.Instantiate(configuration.PlayerPrefab, sceneData.PlayerSpawnPoint.position, Quaternion.identity);
            player.playerTransform = playerGameObject.transform;
            player.playerSpeed = configuration.PlayerSpeed;
            player.CharacterController = playerGameObject.GetComponent<CharacterController>();
            player.playerAnimator = playerGameObject.GetComponent<Animator>();

            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            var weaponEntity = _world.NewEntity();
            var weaponGameObject = Object.Instantiate(WeaponSettings.WeaponPrefab, playerGameObject.GetComponent<WeaponParent>().Pistol);
            //weaponGameObject.SetActive(false);
            ref var weapon = ref weaponEntity.Get<Weapon>();
            weapon.owner = playerEntity;
            weapon.projectilePrefab = WeaponSettings.ProjectilePrefab;
            weapon.projectileRadius = WeaponSettings.ProjectileRadius;
            weapon.projectileSocket = WeaponSettings.ProjectileSocket;
            weapon.projectileSpeed = WeaponSettings.ProjectileSpeed;
            weapon.totalAmmo = WeaponSettings.TotalAmmo;
            weapon.weaponDamage = WeaponSettings.WeaponDamage;
            weapon.currentInMagazine = WeaponSettings.CurrentInMagazine;
            weapon.maxInMagazine = WeaponSettings.MaxInMagazine;
        }
    }
}
