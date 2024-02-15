using System.Collections;
using UnityEngine;
using UnityEngine.AI; // For NavMeshAgent
using UnityEngine.SceneManagement;

namespace YaoLu
{
    public abstract class BaseEnemyState : IState
    {
        protected Animator animator;
        protected NavMeshAgent navMeshAgent;
        protected Transform playerTransform;
        protected float wanderRadius;
        protected float chaseDistance;

        // Constructor with additional parameters
        public BaseEnemyState(Animator animator, NavMeshAgent navMeshAgent = null, Transform playerTransform = null, float wanderRadius = 9, float chaseDistance = 0)
        {
            this.animator = animator;
            this.navMeshAgent = navMeshAgent;
            this.playerTransform = playerTransform;
            this.wanderRadius = wanderRadius;
            this.chaseDistance = chaseDistance;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public abstract void Update();
    }

    // Idle State
    public class EnemyIdleState : BaseEnemyState
    {
        private float timer;
        private float idleDuration;
        private Enemy enemy;

        public EnemyIdleState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, float wanderRadius) : base(animator, navMeshAgent)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.Play("Idle");
            timer = 0f;
            idleDuration = Random.Range(1f, 3f); // Idle for 1-3 seconds
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= idleDuration)
            {
                // After idling, prepare to wander by setting a random direction
                SetRandomDirection();
                enemy.ChangeState(enemy.wanderState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void SetRandomDirection()
        {
            // Randomly rotate the enemy to face a new direction
            float randomAngle = Random.Range(0f, 360f);
            enemy.transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);
        }
    }


    // Wander State
    public class EnemyWanderState : BaseEnemyState
    {
        private float timer;
        private float wanderDuration;
        private Enemy enemy;

        // Modified constructor to include playerTransform and chaseDistance
        public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, float wanderRadius, Transform playerTransform, float chaseDistance)
            : base(animator, navMeshAgent, playerTransform, wanderRadius, chaseDistance)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.Play("Walk");
            navMeshAgent.isStopped = false;
            timer = 0f;
            wanderDuration = Random.Range(2f, 5f); // Wander for 2-5 seconds
                                                   // Move forward in the currently facing direction
            navMeshAgent.SetDestination(enemy.transform.position + enemy.transform.forward * wanderRadius);
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= wanderDuration)
            {
                enemy.ChangeState(enemy.idleState);
            }

            // Line-of-sight check
            if (Vector3.Distance(enemy.transform.position, playerTransform.position) <= chaseDistance)
            {
                RaycastHit hit;
                Vector3 directionToPlayer = playerTransform.position - enemy.transform.position;
                if (Physics.Raycast(enemy.transform.position, directionToPlayer.normalized, out hit, chaseDistance))
                {
                    if (hit.transform == playerTransform)
                    {
                        // Player is within line of sight and chasing distance, change state to chasing
                        enemy.ChangeState(enemy.chasingState);
                    }
                }
            }
        }


        public override void OnExit()
        {
            base.OnExit();
            navMeshAgent.isStopped = false ; // Ensure the agent stops moving when exiting this state
        }
    }




    // Chasing State
    public class EnemyChasingState : BaseEnemyState
    {
        private Enemy enemy;

        public EnemyChasingState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform,
            float chaseDistance) : base(animator, navMeshAgent, playerTransform, 0, chaseDistance) {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            animator.Play("Run");
        }

        public override void Update()
        {
            
            navMeshAgent.destination = playerTransform.position;

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, playerTransform.position);

            // Line-of-sight check
            RaycastHit hit;
            Vector3 directionToPlayer = playerTransform.position - enemy.transform.position;
            bool playerVisible = Physics.Raycast(enemy.transform.position, directionToPlayer.normalized, out hit, chaseDistance) && hit.transform == playerTransform;

            if (distanceToPlayer > chaseDistance || !playerVisible)
            {
                // Player escaped or is not in direct line of sight, switch back to wander state
                enemy.ChangeState(enemy.wanderState);
            }
            else if (distanceToPlayer < 0.2f && playerVisible) // Ensuring player is still visible
            {
                // Close enough to engage in battle and player is visible, switch to battle state
                enemy.ChangeState(enemy.battleState);
                //SceneManager.LoadScene("MainMenu");
            }
        }

    }
    public enum BattleOutcome
    {
        Win,
        Lose
    }

    public class EnemyBattleState : BaseEnemyState
    {
        private Enemy enemy;
        private BattleOutcome outcome; // Add this line

        public EnemyBattleState(Enemy enemy, Animator animator) : base(animator)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            SceneManager.LoadScene("BattleScene");
           // ProcessBattleOutcome()
        }



        private void ProcessBattleOutcome()
        {
            if (outcome == BattleOutcome.Win)
            {
                // Assuming you have a method to load the main scene with the player having defeated the enemy
                SceneManager.LoadScene("SampleScene"); // Win returns to the main scene
            }
            else if (outcome == BattleOutcome.Lose)
            {
                //SceneManager.LoadScene("MainMenu");  Lose goes to the main menu
            }
        }

        public override void Update()
        {
            // Battle
        }
    }

    public class EnemyHurtState : BaseEnemyState
    {
        public EnemyHurtState(Enemy enemy, Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // hurt
            animator.Play("Hurt");
        }

        public override void Update()
        {

        }
    }
    public class EnemyDieState : BaseEnemyState
    {
        public EnemyDieState(Enemy enemy,Animator animator) : base(animator) { }
        public override void OnEnter()
        {
         
        }

        public override void Update()
        {
        }
    }
}
