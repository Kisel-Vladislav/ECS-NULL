using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerC> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);

                Plane playerPlane = new Plane(Vector3.up, player.playerTransform.position);
                Ray ray = _sceneData.MainCamera.ScreenPointToRay(Input.mousePosition);
                if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

                player.playerTransform.forward = ray.GetPoint(hitDistance) - player.playerTransform.position;
            }
        }
    }
}
