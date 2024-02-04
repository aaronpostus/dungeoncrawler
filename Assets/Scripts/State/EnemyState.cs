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
        public EnemyIdleState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, float wanderRadius) : base(animator, navMeshAgent, null, wanderRadius) { }

        public override void OnEnter()
        {
            animator.Play("Idle");
        }

        public override void Update() { }
    }

 // Wander State
    public class EnemyWanderState : BaseEnemyState
    {
        public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, float wanderRadius) : base(animator, navMeshAgent, null, wanderRadius) { }

        public override void OnEnter()
        {
            animator.Play("Walk");
            // Implement wandering logic using navMeshAgent and wanderRadius
        }

        public override void Update() { }
    }

 // Chasing State
    public class EnemyChasingState : BaseEnemyState
    {
        public EnemyChasingState(Enemy enemy, Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform, float chaseDistance) : base(animator, navMeshAgent, playerTransform, 0, chaseDistance) { }

        public override void OnEnter()
        {
            animator.Play("Run");
            // Implement chasing logic using navMeshAgent, playerTransform, and chaseDistance
        }

        public override void Update() { }
    }


    public class EnemyAttackState : BaseEnemyState
    {
        public EnemyAttackState(Enemy enemy,Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // play attack
            animator.Play("Kick");
            WaitAttack();
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
