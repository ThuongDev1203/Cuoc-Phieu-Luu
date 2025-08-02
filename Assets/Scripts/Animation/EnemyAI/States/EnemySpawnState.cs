using System.Collections;
using System.Collections.Generic;
using Animation.EnemyAI.Manager;
using UnityEngine;

namespace Animation.EnemyAI.States
{
    /// <summary>
    /// EnemySpawnState class for handling enemy spawning behavior
    /// </summary>
    public class EnemySpawnState : IEnemyState
    {
        private readonly EnemyAIManager enemy;

        public EnemySpawnState(EnemyAIManager enemy)
        {
            this.enemy = enemy;
        }

        public void EnterState()
        {
            enemy.SetAnimation("Spawn");
        }

        public void UpdateState()
        {
            if (enemy.IsCurrentAnimFinished())
            {
                enemy.StateMachine.ChangeState(new EnemyWalkState(enemy));
            }
        }

        public void ExitState() { }
    }

}
