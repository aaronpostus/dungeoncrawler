using System.Collections;
using UnityEngine;
using UnityEngine.AI; // For NavMeshAgent

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
                // After wandering, go back to idle state
                enemy.ChangeState(enemy.idleState);
            }

            // Check if the player is within the chasing distance
            if (Vector3.Distance(enemy.transform.position, playerTransform.position) <= chaseDistance)
            {
                // If within chasing distance, change state to chasing
                enemy.ChangeState(enemy.chasingState);
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

            // Check the distance to the player
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, playerTransform.position);

            // Check if the player is out of chasing distance
            if (distanceToPlayer > chaseDistance)
            {
                // Player escaped, switch back to wander state
                enemy.ChangeState(enemy.wanderState);
            }
            else if (distanceToPlayer < 0.2f) // Check if within battle range
            {
                // Close enough to engage in battle, switch to battle state
                enemy.ChangeState(enemy.battleState);
            }
        }
    }


    public class EnemyBattleState : BaseEnemyState
    {
        private Enemy enemy;

        public EnemyBattleState(Enemy enemy,Animator animator) : base(animator) {
        this.enemy = enemy;
        }
        public override void OnEnter()
        {
            
        }

        private IEnumerator WaitAttack()
        {
            yield return new WaitForSeconds(1.2f);
        }
        public override void Update()
        {
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
            // Die
            animator.Play("Die");
        }

        public override void Update()
        {
        }
    }
}
