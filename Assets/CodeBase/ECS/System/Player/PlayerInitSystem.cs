using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using CodeBase.Infrastructure.Factory;
using CodeBase.UI;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        private SceneData sceneData;
        private Hud Hud;
        private IEntityFactory _entityFactory;
        private IEntityViewFactory _entityViewFactory;

        public void Init()
        {
            var player = _entityFactory.CreatePlayer();
            _entityViewFactory.CreatePlayer(ref player, sceneData.PlayerSpawnPoint.position, Quaternion.identity);

            var weapon = _entityFactory.SetupWeapon(ref player);
            _entityViewFactory.SetupWeapon(ref player);

            ref var weaponData = ref weapon.Get<Weapon>();
            Hud.SetAmmo(weaponData.currentInMagazine, weaponData.maxInMagazine);
        }
    }
}
