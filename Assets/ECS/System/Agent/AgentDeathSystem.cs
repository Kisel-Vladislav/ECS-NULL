using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentDeathSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, TransformRef, AnimatorRef,DeathEvent> deadEnemies;

        public void Run()
        {
            foreach (var i in deadEnemies)
            {
                ref var transform = ref deadEnemies.Get2(i);
                ref var animatorRef = ref deadEnemies.Get3(i);
                animatorRef.animator.SetTrigger("Die");

                ref var entity = ref deadEnemies.GetEntity(i);

                var aggro = transform.transform.gameObject.GetComponentInChildren<Aggro>();
                aggro.gameObject.SetActive(false);

                entity.Destroy();
            }
        }
    }
}
