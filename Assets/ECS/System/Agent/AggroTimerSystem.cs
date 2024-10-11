using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AggroTimerSystem : IEcsRunSystem
    {
        private EcsFilter<AggroTimer> _filter;
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
                    RemoveAggroTimer(i);
            }
        }
        private void RemoveAggroTimer(int i)
        {
            ref var entity = ref _filter.GetEntity(i);
            entity.Del<AggroTimer>();
        }
    }
}
