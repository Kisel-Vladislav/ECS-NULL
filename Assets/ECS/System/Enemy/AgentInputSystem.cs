using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Enemy;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentInputSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var agent = ref _filter.Get1(i);

                ref var input = ref entity.Get<MoveInput>();
                input.vector = agent.navMeshAgent.desiredVelocity;
            }
        }
    }
}
