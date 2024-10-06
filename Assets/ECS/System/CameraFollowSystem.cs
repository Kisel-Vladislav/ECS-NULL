using CodeBase.ECS.Component;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerMove, TransformRef> _filter;
        private SceneData _sceneData;
        private StaticData _staticData;

        private Vector3 _currentVelocity;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var transform = ref _filter.Get2(i);

                var currentPosition = _sceneData.MainCamera.transform.position;
                currentPosition = Vector3.SmoothDamp(currentPosition, transform.transform.position + _staticData.FollowOffset,ref _currentVelocity, _staticData.SmoothTime);
                _sceneData.MainCamera.transform.position = currentPosition;
            }
        }
    }
}
