using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.System
{
    public class GravitySystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent.PlayerC> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                var gravity = Physics.gravity;
                player.CharacterController.Move(gravity * Time.deltaTime);
            }
        }
    }
}
