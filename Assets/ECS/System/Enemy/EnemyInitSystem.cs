using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Enemy;
using CodeBase.ECS.Data;
using CodeBase.ECS.PlayerComponent;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.ECS.System.Enemy
{
    public class EnemyIdleSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, AnimatorRef, Idle> calmEnemies;
        private RuntimeData runtimeData;

        public void Run()
        {
            foreach (var i in calmEnemies)
            {
                ref var enemy = ref calmEnemies.Get1(i);
                ref var player = ref runtimeData.playerEntity.Get<PlayerC>();
                ref var animatorRef = ref calmEnemies.Get2(i);

                if ((enemy.transform.position - player.playerTransform.position).sqrMagnitude <= enemy.triggerDistance * enemy.triggerDistance)
                {
                    ref var entity = ref calmEnemies.GetEntity(i);
                    entity.Del<Idle>();
                    ref var follow = ref entity.Get<Follow>();
                    follow.target = runtimeData.playerEntity;
                    animatorRef.animator.SetBool("Running", true);
                }
            }
        }
    }
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;

        public void Init()
        {
            foreach (var enemyView in Object.FindObjectsOfType<EnemyView>())
            {
                var enemyEntity = ecsWorld.NewEntity();

                ref var enemy = ref enemyEntity.Get<EnemyComponent>();
                ref var health = ref enemyEntity.Get<Health>();
                ref var animatorRef = ref enemyEntity.Get<AnimatorRef>();

                enemyEntity.Get<Idle>();
                enemyView.entity = enemyEntity;

                health.value = enemyView.startHealth;
                enemy.damage = enemyView.damage;
                enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
                enemy.navMeshAgent = enemyView.navMeshAgent;
                enemy.transform = enemyView.transform;
                enemy.meleeAttackInterval = enemyView.meleeAttackInterval;
                enemy.triggerDistance = enemyView.triggerDistance;
                animatorRef.animator = enemyView.animator;
            }
        }
    }
}
