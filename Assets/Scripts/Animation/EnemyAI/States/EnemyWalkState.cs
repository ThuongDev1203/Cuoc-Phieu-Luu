using System.Collections;
using System.Collections.Generic;
using Animation.EnemyAI.Manager;
using UnityEngine;

namespace Animation.EnemyAI.States
{
    /// <summary>
    /// EnemyWalkState class for handling enemy walking behavior
    /// </summary>
    public class EnemyWalkState : IEnemyState
    {
        private readonly EnemyAIManager enemy;

        public EnemyWalkState(EnemyAIManager enemy)
        {
            this.enemy = enemy;
        }

        public void EnterState()
        {
            enemy.SetAnimation("Walk");
        }

        public void UpdateState()
        {
            Transform target = enemy.GetCurrentTarget();
            Vector2 direction = target.position - enemy.transform.position;

            enemy.Flip(direction);
            enemy.MoveTowards(target.position);

            if (Vector2.Distance(enemy.transform.position, target.position) < 0.1f)
            {
                enemy.SwitchTarget();
            }
        }


        public void ExitState() { }
    }

}
