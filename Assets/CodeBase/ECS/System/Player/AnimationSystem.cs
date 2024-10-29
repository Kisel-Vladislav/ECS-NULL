using CodeBase.ECS.Component;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.PlayerSystem
{
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<MoveInput,TransformRef,AnimatorRef> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var animator = ref filter.Get3(i);
                ref var input = ref filter.Get1(i);
                ref var transform = ref filter.Get2(i); 

                float vertical = Vector3.Dot(input.vector.normalized, transform.transform.forward);
                float horizontal = Vector3.Dot(input.vector.normalized, transform.transform.right);
                animator.animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
                animator.animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
                //animator.animator.SetBool("IsAiming", input.IsAimButtonPressed);
            }
        }
    }
}
