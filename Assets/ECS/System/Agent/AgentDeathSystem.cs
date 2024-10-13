using CodeBase.ECS.Component;
using CodeBase.ECS.Component.Agent;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentDeathSystem : IEcsRunSystem
    {
        private EcsFilter<AgentComponent, TransformRef, AnimatorRef,DeathEvent> deadAgents;

        public void Run()
        {
            foreach (var i in deadAgents)
            {
                ref var entity = ref deadAgents.GetEntity(i);

                ref var agentComponent = ref deadAgents.Get1(i);
                ref var animatorRef = ref deadAgents.Get3(i);
                ref var transform = ref deadAgents.Get2(i);

                agentComponent.navMeshAgent.SetDestination(transform.transform.position);
                agentComponent.navMeshAgent.enabled = false;

                animatorRef.animator.SetTrigger("Die");


                var aggro = transform.transform.gameObject.GetComponentInChildren<Aggro>();
                aggro.gameObject.SetActive(false);

                entity.Destroy();
            }
        }
    }
}
