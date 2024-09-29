using CodeBase.ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.System
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<Component.Player> _filter;
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
