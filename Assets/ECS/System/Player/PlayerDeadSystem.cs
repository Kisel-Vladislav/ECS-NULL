using CodeBase.ECS.Component;
using Leopotam.Ecs;
using CodeBase.ECS.PlayerComponent;
using CodeBase.Infrastructure.States;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private GameStateMachine _gameStateMachine;

        private EcsFilter<AnimatorRef, DeathEvent,PlayerTag> deadEnemies;

        public void Run()
        {
            foreach (var i in deadEnemies)
            {
                ref var animatorRef = ref deadEnemies.Get1(i);
                animatorRef.animator.SetTrigger("Die");
                ref var entity = ref deadEnemies.GetEntity(i);

                entity.Destroy();
                _gameStateMachine.Enter<GameLoopFailState>();
            }
        }
    }
}
