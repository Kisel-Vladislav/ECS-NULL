using CodeBase.ECS.Component;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerC,PlayerInputData,TransformRef> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);
                ref var transform = ref filter.Get3(i); 

                float vertical = Vector3.Dot(input.moveInput.normalized, transform.transform.forward);
                float horizontal = Vector3.Dot(input.moveInput.normalized, transform.transform.right);
                player.playerAnimator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
                player.playerAnimator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
                player.playerAnimator.SetBool("IsAiming", input.IsAimButtonPressed);
            }
        }
    }
}
