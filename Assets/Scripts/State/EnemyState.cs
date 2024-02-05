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
        public BaseEnemyState(Animator animator, NavMeshAgent navMeshAgent = null, Transform playerTransform = null, float wanderRadius = 0, float chaseDistance = 0)
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
                // After idling, go to wander state
                enemy.ChangeState(enemy.wanderState);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            // Prepare for next idle state entry, if needed
        }
    }


    // Wander State
    public class EnemyWanderState : BaseEnemyState
    {
        private float timer;
        private float wanderDuration;
        private Enemy enemy;

        public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, float wanderRadius) : base(animator, navMeshAgent)
        {
            this.enemy = enemy;
            this.wanderRadius = wanderRadius;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.Play("Walk");
            navMeshAgent.isStopped = false;
            SetRandomDestination();
            timer = 0f;
            wanderDuration = Random.Range(2f, 5f); // Wander for 2-5 seconds
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= wanderDuration)
            {
                // After wandering, go back to idle state
                enemy.ChangeState(enemy.idleState);
            }
        }

        private void SetRandomDestination()
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += navMeshAgent.transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, -1))
            {
                finalPosition = hit.position;
            }
            navMeshAgent.SetDestination(finalPosition);
        }

        public override void OnExit()
        {
            base.OnExit();
            navMeshAgent.isStopped = true; // Ensure the agent stops moving when exiting this state
        }
    }



    // Chasing State
    public class EnemyChasingState : BaseEnemyState
    {
        public EnemyChasingState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform, float chaseDistance) : base(animator, navMeshAgent, playerTransform, 0, chaseDistance) { }

        public override void OnEnter()
        {
            animator.Play("Run");
        }

        public override void Update() {
            navMeshAgent.destination = playerTransform.position;
        }
    }


    public class EnemyAttackState : BaseEnemyState
    {
        public EnemyAttackState(Enemy enemy,Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // play attack
            animator.Play("Kick");
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
