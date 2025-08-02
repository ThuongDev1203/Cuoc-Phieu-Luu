using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation.EnemyAI.States
{
    /// <summary>
    /// EnemyStateMachine class for managing enemy AI states
    /// </summary>
    public class EnemyStateMachine : MonoBehaviour
    {
        private IEnemyState currentState;

        public void ChangeState(IEnemyState newState)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }

            currentState = newState;
            currentState.EnterState();
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }
    }
}
