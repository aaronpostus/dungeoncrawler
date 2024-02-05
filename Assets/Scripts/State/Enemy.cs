using UnityEngine;
using UnityEngine.AI; // Import the AI namespace for NavMeshAgent
using YaoLu;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent
    public Transform playerTransform; // Reference to the player's transform
    public float wanderRadius = 10f;
    public float chaseDistance = 15f;
    public float WanderSpeed = 4f;
    public float ChaseSpeed = 8f;
    public int life = 100;

    // States
    public EnemyAttackState attackState;
    public EnemyWanderState wanderState;
    public EnemyDieState dieState;
    public EnemyHurtState hurtState;
    public EnemyChasingState chasingState;
    public EnemyIdleState idleState;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Initialize the NavMeshAgent
        stateMachine = new StateMachine();
        idleState = new EnemyIdleState(this, animator, navMeshAgent, wanderRadius);
        wanderState = new EnemyWanderState(this, animator, navMeshAgent, wanderRadius);
        chasingState = new EnemyChasingState(this, animator, navMeshAgent, playerTransform, chaseDistance);
        attackState = new EnemyAttackState(this,animator);
        dieState = new EnemyDieState(this,animator);
        hurtState = new EnemyHurtState(this, animator);
        stateMachine.ChangeState(chasingState); // Start in the idle
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void ChangeState(IState newState)
    {
        stateMachine.ChangeState(newState);
    }

    public void Hurt()
    {
        life -= 10;
        if (life <= 0)
        {
            ChangeState(dieState);
        }
        else
        {
            ChangeState(hurtState);
        }
    }
}
