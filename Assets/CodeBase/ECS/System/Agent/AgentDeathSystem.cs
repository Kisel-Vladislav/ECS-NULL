﻿using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentDeathSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, TransformRef, AnimatorRef,CharacterControllerComponent,DeathEvent> deadAgents;

        public void Run()
        {
            foreach (var i in deadAgents)
            {
                ref var entity = ref deadAgents.GetEntity(i);

                ref var agentComponent = ref deadAgents.Get1(i);
                ref var animatorRef = ref deadAgents.Get3(i);
                ref var characterController = ref deadAgents.Get4(i);
                ref var transform = ref deadAgents.Get2(i);

                agentComponent.navMeshAgent.enabled = false;

                animatorRef.animator.SetTrigger("Die");

                characterController.CharacterController.enabled = false;

                var aggro = transform.transform.gameObject.GetComponentInChildren<DetectionZone>();
                aggro.gameObject.SetActive(false);

                entity.Destroy();
            }
        }
    }
}
