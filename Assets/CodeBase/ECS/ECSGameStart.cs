using CodeBase.ECS.Component;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.PlayerSystem;
using CodeBase.ECS.System;
using CodeBase.ECS.System.Agent;
using CodeBase.ECS.WeaponComponent;
using CodeBase.ECS.WeaponSystem;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace CodeBase.ECS
{
    public class ECSGameStart : MonoBehaviour
    {
        public StaticData Configuration;
        public SceneData SceneData;
        public WeaponSettings WeaponSettings;

        private RuntimeData _runtimeData;

        private EcsWorld _world;
        private EcsSystems _systems;
        private Hud _hud;
        private GameStateMachine _gameStateMachine;
        private IEntityViewFactory _entityViewFactory;
        private IEntityFactory _entityFactory;

        [Inject]
        public void Construct(IUIFactory uIFactory, GameStateMachine gameStateMachine, IEntityViewFactory entityViewFactory, IEntityFactory entityFactory)
        {
            _hud = uIFactory.CreateHud();
            _gameStateMachine = gameStateMachine;
            _entityViewFactory = entityViewFactory;
            _entityFactory = entityFactory;
        }
        private void Start()
        {
            
            _world = new EcsWorld();
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
#endif

            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems.ConvertScene();
            _runtimeData = new RuntimeData();

            _entityFactory.World = _world;

            AddInitSystems();
            AddInputSystems();
            AddMoveSystems();
            AddAttackSystems();
            AddDeathSystems();
            AddAnimationSystems();

            //AddSystems();
            Inject();

            _systems.Init();
        }

        private void AddAnimationSystems()
        {
            _systems
                .Add(new AimSystem())
                .Add(new AnimationSystem())
                ;
        }

        private void AddDeathSystems()
        {
            _systems
                .Add(new AgentDeathSystem())
                .Add(new PlayerDeathSystem())
                ;
        }

        private void AddAttackSystems()
        {
            _systems
                .Add(new AgentAttackSystem())
                .Add(new WeaponBlockSystem())
                .Add(new WeaponShootSystem())
                .Add(new ReloadingSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new ProjectileHitSystem())
                .Add(new DamageSystem())
                .Add(new AggroTimerSystem())
                ;
        }

        private void AddMoveSystems()
        {
            _systems
                .Add(new GravitySystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerDodgeSystem())
                .Add(new LookAtSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerRotationSystem())
                .Add(new AgentFollowSystem())
                //.Add(new AggroTimerSystem())
                .Add(new AgentDetectionZoneSystem())
                .Add(new AgentAggroSystem())
                .Add(new ProjectileMoveSystem())
                ;
        }

        private void AddInputSystems()
        {
            _systems
                .OneFrame<TryDodge>()
                .OneFrame<TryReload>()
                .OneFrame<TryAim>()
                .Add(new PlayerInputSystem())
                .Add(new AgentInputSystem())
                .Add(new ReloadingSystem())
                ;
        }

        private void AddInitSystems()
        {
            _systems
                .Add(new PlayerInitSystem())
                .Add(new AgentInitSystem())
                ;
        }

        private void Inject()
        {
            _systems.Inject(Configuration)
                    .Inject(SceneData)
                    .Inject(_runtimeData)
                    .Inject(WeaponSettings)
                    .Inject(_hud)
                    .Inject(_gameStateMachine)
                    .Inject(_entityFactory)
                    .Inject(_entityViewFactory)
                    ;
        }

        private void AddSystems()
        {
            _systems.Add(new PlayerInitSystem())
                .Add(new AgentInitSystem())
                .OneFrame<TryReload>()
                .OneFrame<TryAim>()
                .Add(new PlayerInputSystem())
                .Add(new GravitySystem())
                .Add(new PlayerMoveSystem())
                .Add(new AgentAggroSystem())
                .Add(new AgentInputSystem())
                .Add(new AgentAttackSystem())
                .Add(new AimSystem())
                .Add(new LookAtSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerRotationSystem())
                .Add(new WeaponBlockSystem())
                .Add(new WeaponShootSystem())
                .Add(new ReloadingSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new ProjectileHitSystem())
                .Add(new DamageSystem())
                .Add(new AgentDeathSystem())
                .Add(new PlayerDeathSystem())
                .Add(new ProjectileMoveSystem())
                .Add(new AnimationSystem())
                .Add(new AgentFollowSystem())
                .Add(new AggroTimerSystem())
                ;
        }

        private void Update()
        {
            _systems?.Run();
        }
        private void OnDestroy()
        {
            _world?.Destroy();
            _systems?.Destroy();

            _world = null;
            _systems = null;
        }
    }
}
