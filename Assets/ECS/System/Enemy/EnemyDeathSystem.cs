using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Enemy;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class EnemyDeathSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, DeathEvent, AnimatorRef> deadEnemies;

        public void Run()
        {
            foreach (var i in deadEnemies)
            {
                ref var enemy = ref deadEnemies.Get1(i);
                ref var animatorRef = ref deadEnemies.Get3(i);

                animatorRef.animator.SetTrigger("Die");

                ref var entity = ref deadEnemies.GetEntity(i);

                var aggro = enemy.transform.gameObject.GetComponentInChildren<Aggro>();
                aggro.gameObject.SetActive(false);

                entity.Destroy();
            }
        }
    }
}
