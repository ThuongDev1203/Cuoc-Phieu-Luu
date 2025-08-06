using System.Collections.Generic;
using Animation.Boss5State.States.bossState;


namespace Animation.Boss5State.States
{
    public class BossStateMachine
    {
        public BosState CurrentState { get; private set; }
        private readonly Dictionary<System.Type, BosState> _states = new Dictionary<System.Type, BosState>();

        public void Initialize(BosState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(BosState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void AddState(BosState state)
        {
            var type = state.GetType();
            if (!_states.ContainsKey(type))
            {
                _states.Add(type, state);
            }
        }

        public void Update()
        {
            CurrentState?.LogicUpdate();
        }

        public T GetState<T>() where T : BosState
        {
            return _states[typeof(T)] as T;
        }
        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
}
