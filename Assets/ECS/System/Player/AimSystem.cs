using CodeBase.ECS.Component;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System
{
    public class AimSystem : IEcsRunSystem
    {
        private EcsFilter<TryAim, AnimatorRef> _tryAimFilter;
        private EcsFilter<AimFinished, AnimatorRef> _finishedAimFilter;
        public void Run()
        {
            foreach (var i in _tryAimFilter)
            {
                ref var animator = ref _tryAimFilter.Get2(i);
                Debug.Log("Try");
                animator.animator.SetBool("IsAiming", true);
            }
            foreach (var i in _finishedAimFilter)
            {
                ref var entity = ref _finishedAimFilter.GetEntity(i);

                ref var animator = ref _finishedAimFilter.Get2(i);
                animator.animator.SetBool("IsAiming",false);

                entity.Del<AimFinished>();
            }
        }
    }
}
