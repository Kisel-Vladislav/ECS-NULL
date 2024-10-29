using CodeBase.ECS.Component;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System
{
    public class PlayerDodgeSystem : IEcsRunSystem
    {
        private const float DodgeCooldown = 3f;
        private const float DodgeSpeed = 5f;

        private EcsFilter<CharacterControllerComponent, Dodging, MoveInput, TransformRef,PlayerTag> _dodgingfilter;

        private EcsFilter<TryDodge, AnimatorRef, MoveInput, TransformRef>
            .Exclude<Dodging> _tryDodge;

        private EcsFilter<BlockDodgeDuration> _blockFilter;

        private EcsFilter<DodgingFinished> _dodgingFinished;

        public void Run()
        {
            foreach (var i in _dodgingFinished)
            {
                ref var entity = ref _blockFilter.GetEntity(i);

                entity.Del<Dodging>();
                entity.Del<DodgingFinished>();
            }

            foreach (var i in _blockFilter)
            {
                ref var entity = ref _blockFilter.GetEntity(i);
                ref var blockDuration = ref _blockFilter.Get1(i);

                blockDuration.Duration -= Time.deltaTime;

                if (blockDuration.Duration <= 0f)
                {
                    entity.Del<BlockDodgeDuration>();
                }
            }

            foreach (var i in _tryDodge)
            {
                ref var entity = ref _tryDodge.GetEntity(i);
                ref var animatorRef = ref _tryDodge.Get2(i);
                ref var transformRef = ref _tryDodge.Get4(i);
                ref var moveInput = ref _tryDodge.Get3(i);

                ref var dodging = ref entity.Get<Dodging>();

                var dodgeDirection = moveInput.vector.normalized;

                animatorRef.animator.SetTrigger("Dodge");

            }

            foreach (var i in _dodgingfilter)
            {
                ref var entity = ref _dodgingfilter.GetEntity(i);
                ref var characterController = ref _dodgingfilter.Get1(i);
                ref var moveInput = ref _dodgingfilter.Get3(i);
                ref var transformRef = ref _dodgingfilter.Get4(i);

                var dodgeDirection = moveInput.vector.normalized;
                characterController.CharacterController.Move(dodgeDirection* DodgeSpeed * Time.deltaTime);
                ref var blockDodgeDuration = ref _dodgingfilter.GetEntity(i).Get<BlockDodgeDuration>();
                blockDodgeDuration.Duration = DodgeCooldown;
            }
        }
    }
}
