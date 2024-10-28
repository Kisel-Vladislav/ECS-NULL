using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System
{
    public class GravitySystem : IEcsRunSystem
    {
        private EcsFilter<CharacterControllerComponent>
            .Exclude<AgentComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var characterController = ref _filter.Get1(i);
                var gravity = Physics.gravity;
                characterController.CharacterController.Move(gravity * Time.deltaTime);
            }
        }
    }
}
