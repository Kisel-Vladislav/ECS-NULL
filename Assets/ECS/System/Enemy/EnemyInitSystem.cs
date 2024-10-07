using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Enemy;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Enemy
{
    public class EnemyAggroSystem : IEcsRunSystem
    {
        private EcsFilter <EnterAggro> _enterFilter;
        private EcsFilter<ExitAggro> _exitFilter;
        public void Run()
        {
            foreach (var i in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(i);
                ref var target = ref _enterFilter.Get1(i);

                ref var follow = ref entity.Get<Follow>();
                follow.target = target.target;
            }
            foreach (var i in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(i);
                ref var timer = ref entity.Get<AggroTimer>();
                timer.Cooldown = 3f; //TO DO to static data or constant
            }
        }
    }
    public class AggroTimerSystem : IEcsRunSystem
    {
        private EcsFilter<AggroTimer> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                timer.Cooldown -= Time.deltaTime;

                if(timer.Cooldown <= 0)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    entity.Del<AggroTimer>();
                }
            }
        }
    }
    public class EnemyFollowSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent, Follow, AnimatorRef> followingEnemies;
        private EcsWorld ecsWorld;

        public void Run()
        {
            foreach (var i in followingEnemies)
            {
                ref var enemy = ref followingEnemies.Get1(i);
                ref var follow = ref followingEnemies.Get2(i);
                ref var animatorRef = ref followingEnemies.Get3(i);

                var targetPos = follow.target.position;
                enemy.navMeshAgent.SetDestination(targetPos);
                //var direction = (targetPos - enemy.transform.position).normalized;
                //direction.y = 0f;

                //if(direction != Vector3.zero)
                //    enemy.transform.forward = direction;
            }
        }
    }
    public class AgentInputSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent> _filter;
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var agent = ref _filter.Get1(i);

                ref var input = ref entity.Get<MoveInput>();
                input.vector = agent.navMeshAgent.desiredVelocity;
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

                var aggro = enemyView.GetComponentInChildren<Aggro>();
                aggro.entity = enemyEntity;

                ref var enemy = ref enemyEntity.Get<EnemyComponent>();
                ref var health = ref enemyEntity.Get<Health>();
                ref var animatorRef = ref enemyEntity.Get<AnimatorRef>();
                ref var transformRef = ref enemyEntity.Get<TransformRef>();
                enemyEntity.Get<Idle>();
                enemyView.entity = enemyEntity;

                health.value = enemyView.startHealth;
                enemy.damage = enemyView.damage;
                enemy.meleeAttackDistance = enemyView.meleeAttackDistance;
                enemy.navMeshAgent = enemyView.navMeshAgent;

                enemy.transform = enemyView.transform;
                transformRef.transform = enemyView.transform;

                enemy.meleeAttackInterval = enemyView.meleeAttackInterval;
                enemy.triggerDistance = enemyView.triggerDistance;
                animatorRef.animator = enemyView.animator;
            }
        }
    }
}
