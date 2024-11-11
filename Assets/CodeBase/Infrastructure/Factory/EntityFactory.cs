using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using CodeBase.Infrastructure.StaticData;
using Leopotam.Ecs;

namespace CodeBase.Infrastructure.Factory
{
    public class EntityFactory : IEntityFactory
    {
        public EcsWorld World { private get; set; }

        private readonly IStaticDataService _staticDataService;

        public EntityFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public EcsEntity CreatePlayer()
        {
            var player = World.NewEntity();
            player.Get<PlayerTag>();

            var playerStaticData = _staticDataService.ForPlayer();

            ref var playerMove = ref player.Get<PlayerMove>();
            playerMove.playerSpeed = playerStaticData.MoveSpeed;

            ref var health = ref player.Get<Health>();
            health.value = playerStaticData.Hp;

            ref var team = ref player.Get<TeamComponent>();
            team.Team = TeamType.Player;

            return player;
        }
        public EcsEntity SetupWeapon(ref EcsEntity owner)
        {
            ref var hasWeapon = ref owner.Get<HasWeapon>();
            var weapon = World.NewEntity();
            hasWeapon.weapon = weapon;

            var weaponStaticData = _staticDataService.ForWeapon();

            ref var weaponData = ref weapon.Get<Weapon>();
            weaponData.owner = owner;
            weaponData.projectilePrefab = weaponStaticData.ProjectilePrefab;
            weaponData.projectileRadius = weaponStaticData.ProjectileRadius;
            weaponData.projectileSpeed = weaponStaticData.ProjectileSpeed;
            weaponData.totalAmmo = weaponStaticData.TotalAmmo;
            weaponData.weaponDamage = weaponStaticData.WeaponDamage;
            weaponData.currentInMagazine = weaponStaticData.CurrentInMagazine;
            weaponData.maxInMagazine = weaponStaticData.MaxInMagazine;
            weaponData.Cooldown = weaponStaticData.Cooldown;

            return weapon;
        }

        public EcsEntity CreateAgent(TeamType teamType)
        {
            var agentEntity = World.NewEntity();

            ref var health = ref agentEntity.Get<Health>();
            ref var team = ref agentEntity.Get<TeamComponent>();

            team.Team = teamType;

            var agentStaticData = _staticDataService.ForAgent(teamType);
            health.value = agentStaticData.Hp;

            return agentEntity;
        }
    }
}

