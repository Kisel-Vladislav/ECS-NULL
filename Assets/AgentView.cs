using UnityEngine;
using UnityEngine.AI;

public class AgentView : MonoBehaviour
{
    public EntityView EntityView;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public CharacterController CharacterController;
    public float meleeAttackDistance;
    public float triggerDistance;
    public float meleeAttackInterval;
    public int startHealth;
    public int damage;
}