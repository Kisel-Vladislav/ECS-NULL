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
        private EcsFilter<TryAggro, TransformRef, TeamComponent> _tryFilter;

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
                ref var team = ref _tryFilter.Get3(i);

                var position = transform.transform.position;


                var colliderCount = Physics.OverlapSphereNonAlloc(position, AggroRange, _hitColliders, LayerMask);

                for (var j = 0; j < colliderCount; j++)
                {
                    var hit = _hitColliders[j];

                    if (hit.gameObject == transform.transform.gameObject || !hit.gameObject.GetComponent<EntityView>().Entity.IsAlive())
                        continue;

                    var targetEntity = hit.gameObject.GetComponent<EntityView>().Entity;
                    if (targetEntity.Has<TeamComponent>())
                    {
                        ref var targetTeam = ref targetEntity.Get<TeamComponent>();
                        if (team.Team != targetTeam.Team)
                        {
                            ref var enterAggro = ref entity.Get<EnterAggro>();
                            enterAggro.target = hit.transform;
                            break;
                        }
                    }

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

                var transformTarget = aggroTarget.target;
                var targetEntity = aggroTarget.target.GetComponent<EntityView>().Entity;

                entity.Del<AggroTimer>();

                ref var follow = ref entity.Get<Follow>();
                follow.Target = transformTarget;
                follow.Entity = targetEntity;

                ref var attackTarget = ref entity.Get<AttackTarget>();
                attackTarget.Target = transformTarget;
                attackTarget.Entity = targetEntity;

                entity.Del<EnterAggro>();
            }
        }
    }
}
