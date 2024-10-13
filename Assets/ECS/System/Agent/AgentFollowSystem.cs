using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentFollowSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, Follow, AnimatorRef> followingEnemies;

        public void Run()
        {
            foreach (var i in followingEnemies)
            {
                ref var entity = ref followingEnemies.GetEntity(i);
                ref var enemy = ref followingEnemies.Get1(i);
                ref var follow = ref followingEnemies.Get2(i);

                if (follow.Entity.IsAlive())
                {
                    enemy.navMeshAgent.enabled = true;
                    var targetPos = follow.Target.position;
                    enemy.navMeshAgent.SetDestination(targetPos);
                }
                else
                {
                    enemy.navMeshAgent.enabled = false;
                    entity.Del<Follow>();
                }

            }
        }
    }
}
