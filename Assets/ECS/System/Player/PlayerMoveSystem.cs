using CodeBase.ECS.Component;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerMove, MoveInput, CharacterControllerComponent,PlayerTag> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                ref var characterController = ref _filter.Get3(i);

                Vector3 direction = (Vector3.forward * input.vector.z + Vector3.right * input.vector.x).normalized;
                characterController.CharacterController.Move(direction * player.playerSpeed * Time.deltaTime);
            }
        }
    }
}
