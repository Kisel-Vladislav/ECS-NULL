using UnityEngine.AI;
using UnityEngine;

namespace CodeBase.ECS.Component.Agent
{
    public struct AgentComponent
    {
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public Transform transform;
        public float meleeAttackDistance;
        public float triggerDistance;
        public float meleeAttackInterval;
        public int damage;
    }
}
