using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.ECS.Component.Agent
{
    public struct DetectionZoneEvent
    {
        public EcsEntity SourceEntity;

        public EcsEntity DetectedEntity;
        public GameObject DetectedObject;
    }
}
