using CodeBase.ECS.Component;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class LookAtSystem : IEcsRunSystem
    {
        private EcsFilter<LookAt, TransformRef> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var at = ref _filter.Get1(i);
                ref var transform = ref _filter.Get2(i);

                transform.transform.LookAt(at.transform);
            }
        }
    }
}
