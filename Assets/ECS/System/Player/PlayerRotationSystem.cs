using CodeBase.ECS.Component;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerMove,TransformRef> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var transform = ref _filter.Get2(i);

                Plane playerPlane = new Plane(Vector3.up, transform.transform.position);
                Ray ray = _sceneData.MainCamera.ScreenPointToRay(Input.mousePosition);
                if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

                transform.transform.forward = ray.GetPoint(hitDistance) - transform.transform.position;
            }
        }
    }
}
