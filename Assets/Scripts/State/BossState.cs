using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YaoLu
{
    public abstract class BaseBossState : IState
    {
        protected Animator animator;
        protected Transform playerTransform;

        // Simplified constructor without NavMeshAgent
        public BaseBossState(Animator animator, Transform playerTransform = null)
        {
            this.animator = animator;
            this.playerTransform = playerTransform;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public abstract void Update();
    }

    // Idle State
    public class BossIdleState : BaseBossState
    {
        private Boss boss;

        public BossIdleState(Boss boss, Animator animator) : base(animator)
        {
            this.boss = boss;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            animator.Play("Idle");
        }

        public override void Update()
        {
            if (playerTransform != null)
            {
                float distanceToPlayer = Vector3.Distance(boss.bossPosition.position, playerTransform.position);
                if (distanceToPlayer <= 2f)
                {
                    boss.ChangeState(boss.battleState);
                }
            }
        }
    }

    // Battle State
    public class BossBattleState : BaseBossState
    {
        private Boss boss;
        public BossBattleState(Boss boss, Animator animator) : base(animator)
        {
            this.boss = boss;
        }

        public override void OnEnter()
        {
            //GameObject.Destroy(boss.gameObject);
            //some animation
            SaveGameManager.instance.TransitionAwayFromMainScene("BattleScene");
        }

        public override void Update()
        {
            
        }
    }

    // Die State
    public class BossDieState : BaseBossState
    {
        private Boss boss;

        public BossDieState(Boss boss, Animator animator) : base(animator)
        {
            this.boss = boss;
        }

        public override void OnEnter()
        {
            animator.Play("Die");
           
        }

        public override void Update()
        {
         
        }
    }

    public class Boss
    {
        public BossIdleState idleState;
        public BossBattleState battleState;
        public BossDieState dieState;
        private StateMachine stateMachine;
        public int life;
        public Transform bossPosition;
        public void ChangeState(BaseBossState newState)
        {
            stateMachine.ChangeState(newState);
        }
        private void Start()
        {

            stateMachine = new StateMachine();
            stateMachine.ChangeState(idleState);
        }
    }
}
