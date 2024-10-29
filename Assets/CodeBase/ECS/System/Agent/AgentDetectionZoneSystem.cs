using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.System.Agent
{
    public class AgentDetectionZoneSystem : IEcsRunSystem
    {
        private const float AggroRange = 10f; // TO DO 
        private const int LayerEnemy = 7;
        private const int LayerPlayer = 8;
        private const int LayerMask = (1 << LayerEnemy) | (1 << LayerPlayer);
        private Collider[] _hitColliders = new Collider[32];

        public EcsFilter<CheckDetectionZone, TransformRef, TeamComponent> checkFilter;
        public EcsFilter<DetectionZoneEvent> filter;
        private EcsWorld _ecsWorld;

        private GameObject _hitGameObject;
        private GameObject _thisGameObject;

        public void Run()
        {
            CheckZone();
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var detectionData = ref filter.Get1(i);

                if (!detectionData.DetectedEntity.IsAlive() ||
                    !detectionData.SourceEntity.IsAlive() ||
                    !detectionData.DetectedEntity.Has<TeamComponent>())
                {
                    entity.Destroy();
                    continue;
                }

                ref var sourceEntityTeam = ref detectionData.SourceEntity.Get<TeamComponent>();
                ref var detectionEntityTeam = ref detectionData.DetectedEntity.Get<TeamComponent>();

                if (detectionEntityTeam.Team == sourceEntityTeam.Team)
                {
                    entity.Destroy();
                    continue;
                }

                switch (sourceEntityTeam.Team)
                {
                    case TeamType.Enemy:
                        HandleEnemy(ref detectionData);
                        break;

                    case TeamType.Ally:
                        HandleAlly(ref detectionData);
                        break;
                }

                entity.Destroy();
            }
        }

        private static void HandleEnemy(ref DetectionZoneEvent detectionData)
        {
            ref var sourceEntity = ref detectionData.SourceEntity;
            
            ref var enterAggro = ref sourceEntity.Get<EnterAggro>();
            enterAggro.target = detectionData.DetectedObject.transform;
        }

        private static void HandleAlly(ref DetectionZoneEvent detectionData)
        {
            ref var detectionEntity = ref detectionData.DetectedEntity;
            ref var sourceEntity = ref detectionData.SourceEntity;

            ref var detectionEntityTeam = ref detectionEntity.Get<TeamComponent>();


            if (detectionEntityTeam.Team == TeamType.Enemy)
            {
                ref var enterAggro = ref sourceEntity.Get<EnterAggro>();
                enterAggro.target = detectionData.DetectedObject.transform;

            }
            else if (detectionEntityTeam.Team == TeamType.Player)
            {
                ref var follow = ref sourceEntity.Get<Follow>();
                follow.Target = detectionData.DetectedObject.transform;
                follow.Entity = detectionData.SourceEntity;
            }
        }

        private void CheckZone()
        {
            foreach (var i in checkFilter)
            {
                ref var entity = ref checkFilter.GetEntity(i);
                ref var transform = ref checkFilter.Get2(i);
                ref var team = ref checkFilter.Get3(i);

                var position = transform.transform.position;
                _thisGameObject = transform.transform.gameObject;

                var colliderCount = Physics.OverlapSphereNonAlloc(position, AggroRange, _hitColliders, LayerMask);

                for (var j = 0; j < colliderCount; j++)
                {
                    var hit = _hitColliders[j];
                    _hitGameObject = hit.gameObject;

                    if (IsSelf() || IsObjectInvalid(_hitGameObject))
                        continue;

                    var targetEntity = hit.gameObject.GetComponent<EntityView>().Entity;

                    var entityEvent = _ecsWorld.NewEntity();
                    ref var enterDetectionZone = ref entityEvent.Get<DetectionZoneEvent>();
                    enterDetectionZone.SourceEntity = entity;
                    enterDetectionZone.DetectedEntity = targetEntity;
                    enterDetectionZone.DetectedObject = hit.gameObject;
                }
                entity.Del<CheckDetectionZone>();
            }
        }

        private bool IsSelf()
        {
            return _hitGameObject == _thisGameObject;
        }

        private bool IsObjectInvalid(GameObject hitGameObject)
        {
            return !hitGameObject.TryGetComponent<EntityView>(out var entityView) || !entityView.Entity.IsAlive();
        }


    }
}
