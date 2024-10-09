using CodeBase.ECS.Component.Enemy;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentAggroSystem : IEcsRunSystem
    {
        private const float AggroCooldown = 3f; //TO DO to static data

        private EcsFilter<EnterAggro> _enterFilter;
        private EcsFilter<ExitAggro> _exitFilter;
        public void Run()
        {
            AggroEnter();
            AggroExit();
        }

        private void AggroExit()
        {
            foreach (var i in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(i);
                ref var timer = ref entity.Get<AggroTimer>();
                timer.Cooldown = AggroCooldown;
            }
        }
        private void AggroEnter()
        {
            foreach (var i in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(i);
                ref var aggroTarget = ref _enterFilter.Get1(i);

                ref var follow = ref entity.Get<Follow>();
                follow.target = aggroTarget.target;
            }
        }
    }
}
