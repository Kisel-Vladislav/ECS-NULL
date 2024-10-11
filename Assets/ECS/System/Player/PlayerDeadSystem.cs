using CodeBase.ECS.Component;
using Leopotam.Ecs;
using CodeBase.ECS.PlayerComponent;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerMove, AnimatorRef, DeathEvent> deadEnemies;

        public void Run()
        {
            foreach (var i in deadEnemies)
            {
                ref var animatorRef = ref deadEnemies.Get2(i);
                animatorRef.animator.SetTrigger("Die");

                ref var entity = ref deadEnemies.GetEntity(i);

                entity.Destroy();
            }
        }
    }
}
