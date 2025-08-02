using Animation.EnemyAI.Manager;

namespace Animation.EnemyAI.States
{
    /// <summary>
    /// EnemyIdleState class for managing enemy idle behavior
    /// </summary>
    public class EnemyIdleState : IEnemyState
    {
        private EnemyAIManager enemy;
        public EnemyIdleState(EnemyAIManager enemy)
        {
            this.enemy = enemy;
        }

        public void EnterState()
        {
            enemy.SetAnimation("Idle");
        }

        public void UpdateState()
        {
            enemy.StateMachine.ChangeState(new EnemyWalkState(enemy));
        }

        public void ExitState() { }
    }
}
