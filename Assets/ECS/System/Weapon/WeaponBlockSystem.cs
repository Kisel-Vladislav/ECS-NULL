using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.WeaponSystem
{
    public class WeaponBlockSystem : IEcsRunSystem
    {
        private EcsFilter<BlockShootDuration> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);
                block.Timer -= Time.deltaTime;

                if (block.Timer <= 0)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    entity.Del<BlockShootDuration>();
                }
            }
        }
    }
}
