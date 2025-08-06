using System.Collections;
using System.Collections.Generic;
using Animation.Boss.state;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
     private Dictionary<System.Type, BossState> _states = new Dictionary<System.Type, BossState>();
        public BossState CurrentState { get; private set; }

        public void Initialize(BossState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }
        public void ChangeState(BossState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
        public void AddState(BossState state)
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

        public T GetState<T>() where T : BossState
        {
            return _states[typeof(T)] as T;
        }


        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
}
