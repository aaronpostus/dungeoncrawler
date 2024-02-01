using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace YaoLu
{
    public abstract class BaseEnemyState : IState
    {
        protected Animator animator;
        public BaseEnemyState(Animator animator)
        {
            this.animator = animator;
        }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public abstract void Update();

    }
    public class EnemyIdleState : BaseEnemyState
    {
        public EnemyIdleState(Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // play idle
        }

        public override void Update()
        {
        }
    }
    public class EnemyWanderState : BaseEnemyState
    {
        public EnemyWanderState(Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // play wander
        }

        public override void Update()
        {
        }
    }
    public class EnemyChasingState : BaseEnemyState
    {
        public EnemyChasingState(Animator animator) : base(animator) { }
        public override void OnEnter()
        {
            // play chasing
        }

        public override void Update()
        {
        }
    }
}
