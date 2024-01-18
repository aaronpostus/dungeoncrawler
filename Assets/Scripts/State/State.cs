using UnityEngine;

namespace YaoLu
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
        void Update();
    }

    public abstract class BaseState : IState
    {
        protected FSMCtrl fsm;
        protected Animator animator;

        public BaseState(FSMCtrl controller, Animator animator)
        {
            this.fsm = controller;
            this.animator = animator;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public abstract void Update();
    }

    public class StateMachine
    {
        private IState currentState;

        public void ChangeState(IState newState)
        {
            if (currentState == newState) return;

            currentState?.OnExit();
            currentState = newState;
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}
