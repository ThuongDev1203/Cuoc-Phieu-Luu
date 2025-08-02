using System.Collections;
using System.Collections.Generic;
using Animation.EnemyAI.Manager;
using UnityEngine;

namespace Animation.EnemyAI.States
{
    /// <summary>
    /// EnemyDeathState class for handling enemy death behavior
    /// </summary>
    public class EnemyDeathState : IEnemyState
    {
        private EnemyAIManager enemy;

        public EnemyDeathState(EnemyAIManager enemy)
        {
            this.enemy = enemy;
        }

        public void EnterState()
        {
            enemy.SetAnimation("Death");
            GameObject.Destroy(enemy.gameObject, 1.5f); // Delay trước khi destroy
        }

        public void UpdateState() { }
        public void ExitState() { }
    }
}
