using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AggroTimerSystem : IEcsRunSystem
    {
        private EcsFilter<AggroTimer,AgentComponent,Follow> _filter;
        public void Run()
        {
            UpdateAggroTimers();
        }

        private void UpdateAggroTimers()
        {
            foreach (var i in _filter)
            {
                ref var timer = ref _filter.Get1(i);
                timer.Cooldown -= Time.deltaTime;

                if (timer.Cooldown <= 0)
                    UnAggro(i);
            }
        }
        private void UnAggro(int i)
        {
            ref var entity = ref _filter.GetEntity(i);
            ref var agentComponent = ref _filter.Get2(i);

            agentComponent.navMeshAgent.enabled = false;

            entity.Del<AggroTimer>();
            entity.Del<Follow>();
        }
    }
}
