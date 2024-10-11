using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    public EntityView EntityView;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public float meleeAttackDistance;
    public float triggerDistance;
    public float meleeAttackInterval;
    public int startHealth;
    public int damage;
}