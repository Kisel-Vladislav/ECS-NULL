using CodeBase.ECS.Data;
using CodeBase.ECS.System;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace CodeBase.ECS.Data
{
}

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

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
            _runtimeData = new RuntimeData();

            AddSystems();
            Inject();

            _systems.Init();
        }

        private void Inject()
        {
            _systems.Inject(Configuration)
                    .Inject(SceneData)
                    .Inject(_runtimeData)
                    .Inject(WeaponSettings)
                    ;
        }

        private void AddSystems()
        {
            _systems.Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new GravitySystem())
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerRotationSystem())
                .Add(new PlayerAnimationSystem())
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
namespace CodeBase.ECS.System
{
}
namespace CodeBase.ECS.Component
{
}
