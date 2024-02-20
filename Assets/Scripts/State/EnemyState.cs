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
            //navMeshAgent.isStopped = false;
            timer = 0f;
            wanderDuration = Random.Range(2f, 5f); // Wander for 2-5 seconds
                       
            // Move forward in the currently facing direction
            //navMeshAgent.SetDestination(enemy.transform.position + enemy.transform.forward * wanderRadius);
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
            //navMeshAgent.isStopped = false ; // Ensure the agent stops moving when exiting this state
        }
    }




    // Chasing State
    public class EnemyChasingState : BaseEnemyState
    {
        private Enemy enemy;

        public EnemyChasingState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform, float chaseDistance)
            : base(animator, navMeshAgent, playerTransform, 0, chaseDistance)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.Play("Run");
            if (navMeshAgent != null && navMeshAgent.enabled)
            {
                navMeshAgent.isStopped = false;
            }
        }

        public override void Update()
        {
            if (navMeshAgent == null || !navMeshAgent.enabled) return;

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, playerTransform.position);
            if (distanceToPlayer <= chaseDistance)
            {
                navMeshAgent.destination = playerTransform.position;
            }

            RaycastHit hit;
            Vector3 directionToPlayer = playerTransform.position - enemy.transform.position;
            if (Physics.Raycast(enemy.transform.position, directionToPlayer.normalized, out hit, chaseDistance))
            {
                if (hit.transform == playerTransform)
                {
                    if (distanceToPlayer < 0.2f)
                    {
                        enemy.ChangeState(enemy.battleState);
                    }
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true; // Ensure the agent stops moving when exiting this state
            }
        }
    }


    public class EnemyBattleState : BaseEnemyState
    {
        private Enemy enemy;
        public BattleSystem battleSystemPrefab; // Reference to a prefab that includes the BattleSystem script
        private BattleSystem battleSystemInstance;

        public EnemyBattleState(Enemy enemy, Animator animator, BattleSystem battleSystemPrefab)
            : base(animator)
        {
            this.enemy = enemy;
            this.battleSystemPrefab = battleSystemPrefab;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering Battle State, disabling NavMeshAgent");
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
            if (battleSystemInstance == null)
            {
                battleSystemInstance = GameObject.Instantiate(battleSystemPrefab);
                // Pass the current enemy to the battle system
                battleSystemInstance.SetEncounteredEnemy(enemy.gameObject);
            }
            else
            {
                // Initialize or reactivate the battle system with the current enemy
                battleSystemInstance.gameObject.SetActive(true);
                battleSystemInstance.SetEncounteredEnemy(enemy.gameObject);
                battleSystemInstance.StartBattle();
            }
            enemy.life -= 10;
        }


        public override void Update()
        {
            if (battleSystemInstance != null)
            {
                switch (battleSystemInstance.state)
                {
                    case BattleSystemState.WON:
                        ProcessWin();
                        break;
                    case BattleSystemState.LOST:
                        ProcessLoss();
                        break;
                }
            }
        }


        private void ProcessWin()
        {
            // Destroy the enemy and clean up the battle
            GameObject.Destroy(enemy.gameObject);
            GameObject.Destroy(battleSystemInstance.gameObject); // Clean up the battle system instance
            // Transition back to another state as needed
        }

        private void ProcessLoss()
        {
            // Handle player defeat, e.g., restart the level, show a defeat screen, etc.
            GameObject.Destroy(battleSystemInstance.gameObject); // Clean up the battle system instance
            SceneManager.LoadScene("MainMenu"); // For example, return to the main menu
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting Battle State, re-enabling NavMeshAgent");
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = true;
                // Make sure the enemy is on the NavMesh
                navMeshAgent.Warp(enemy.transform.position);
            }
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
