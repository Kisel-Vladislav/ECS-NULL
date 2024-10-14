using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentAggroSystem : IEcsRunSystem
    {
        private const float AggroCooldown = 3f; //TO DO to static data
        private const float AggroRange = 10f; // TO DO 
        private const int LayerEnemy = 7;
        private const int LayerPlayer = 8;
        private const int LayerMask = (1 << LayerEnemy) | (1 << LayerPlayer);

        private Collider[] _hitColliders = new Collider[32];

        private EcsFilter<EnterAggro> _enterFilter;
        private EcsFilter<ExitAggro> _exitFilter;
        private EcsFilter<TryAggro, TransformRef> _tryFilter;

        public void Run()
        {
            AggroTry();
            AggroEnter();
            AggroExit();
        }

        private void AggroTry()
        {
            foreach (var i in _tryFilter)
            {
                ref var entity = ref _tryFilter.GetEntity(i);
                ref var transform = ref _tryFilter.Get2(i);

                var position = transform.transform.position;
                

                Physics.OverlapSphereNonAlloc(position, AggroRange, _hitColliders, LayerMask);

                foreach (var hit in _hitColliders)
                {
                    if(hit == null) 
                        break;

                    if(hit.gameObject == transform.transform.gameObject || !hit.gameObject.GetComponent<EntityView>().Entity.IsAlive())
                        continue;

                    ref var enterAggro = ref entity.Get<EnterAggro>();
                    enterAggro.target = hit.transform;
                    _hitColliders = new Collider[32];
                    break;
                }
                entity.Del<TryAggro>();
            }
        }

        private void AggroExit()
        {
            foreach (var i in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(i);
                ref var timer = ref entity.Get<AggroTimer>();
                timer.Cooldown = AggroCooldown;

                entity.Del<ExitAggro>();
            }
        }
        private void AggroEnter()
        {
            foreach (var i in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(i);
                ref var aggroTarget = ref _enterFilter.Get1(i);

                entity.Del<AggroTimer>();

                ref var follow = ref entity.Get<Follow>();
                follow.Target = aggroTarget.target;
                follow.Entity = aggroTarget.target.GetComponent<EntityView>().Entity;

                entity.Del<EnterAggro>();
            }
        }
    }
}
