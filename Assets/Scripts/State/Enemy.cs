using UnityEngine;
using UnityEngine.AI; // Import the AI namespace for NavMeshAgent
using UnityEngine.UI;
using YaoLu;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private StateMachine stateMachine;
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent
    public Transform playerTransform; // Reference to the player's transform
    public float wanderRadius = 8f;
    public float chaseDistance = 8f;
    public float WanderSpeed = 1f;
    public float ChaseSpeed = 4f;
    public int life = 30; // Assuming 'life' is equivalent to 'currentHP'
    public int maxHP = 30;
    public int damage; // Attack Damage
    public int defense;
    public Slider healthSlider; // Assuming you have a slider to show enemy health
    public float visionAngle = 120f; // The total FOV angle
    public int raysCount = 40;
    public string enemytype;

    // States
    public EnemyBattleState battleState;
    public EnemyWanderState wanderState;
    public EnemyDieState dieState;
    public EnemyHurtState hurtState;
    public EnemyChasingState chasingState;
    public EnemyIdleState idleState;
    public BattleSystem battleSystemPrefab;
    public bool TakeDamage(int dmg)
    {
        life -= (dmg - defense);
        healthSlider.value = life;
        if (life <= 0)
        {
            return true; // Enemy is dead
        }
        return false; // Enemy is still alive
    }

    public void Heal(int amount)
    {
        life += amount;
        if (life > maxHP)
        {
            life = maxHP;
        }
        healthSlider.value = life;
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Initialize the NavMeshAgent
        stateMachine = new StateMachine();
        idleState = new EnemyIdleState(this, animator, navMeshAgent, wanderRadius);
        wanderState = new EnemyWanderState(this, animator, navMeshAgent, wanderRadius, playerTransform, chaseDistance);
        chasingState = new EnemyChasingState(this, animator, navMeshAgent, playerTransform, chaseDistance);
        battleState = new EnemyBattleState(this,animator,battleSystemPrefab);
        dieState = new EnemyDieState(this,animator);
        hurtState = new EnemyHurtState(this, animator);
        stateMachine.ChangeState(idleState);
        // Start in the idle
    }

    private void Update()
    {
        stateMachine.Update();
        DrawFieldOfView();
            if (BattleStateManager.IsBattleActive)
            {
                if (navMeshAgent.enabled)
                {
                    navMeshAgent.isStopped = true;
                    navMeshAgent.enabled = false;
                }
            }
            else
            {
                if (!navMeshAgent.enabled)
                {
                    navMeshAgent.enabled = true;
                    navMeshAgent.isStopped = false;
                }
                // Regular enemy behavior
            }
        
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

    //DEBUG
    void DrawFieldOfView()
    {
        Vector3 forward = transform.forward;
        float startAngle = -visionAngle / 2;
        float angleStep = visionAngle / (raysCount - 1);

        for (int i = 0; i < raysCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * forward;
            Debug.DrawLine(transform.position, transform.position + direction * chaseDistance, Color.red);
        }
    }

}
