using UnityEngine.AI;
using UnityEngine;

namespace CodeBase.ECS.Component.Enemy
{
    public struct EnemyComponent
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
