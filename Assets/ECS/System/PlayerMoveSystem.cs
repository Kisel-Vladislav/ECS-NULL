using CodeBase.ECS.Component;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.System
{
    public class AimSystem
    {

    }
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Component.Player, PlayerInputData> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);

                Vector3 direction = (Vector3.forward * input.moveInput.z + Vector3.right * input.moveInput.x).normalized;
                player.CharacterController.Move(direction * player.playerSpeed * Time.deltaTime);
                
            }
        }
    }
}
