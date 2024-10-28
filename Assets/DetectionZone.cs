using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private bool _hasAggroTarget;
    public EcsEntity entity;

    private void OnTriggerEnter(Collider other)
    {
        ref var aggro = ref entity.Get<CheckDetectionZone>();
    }
    private void OnTriggerExit(Collider other)
    {
        ref var aggro = ref entity.Get<ExitAggro>();
        aggro.target = other.transform;
        
    }
}
