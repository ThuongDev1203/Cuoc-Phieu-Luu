
using Annimation.Boss.Manager;

namespace Animation.Boss5State.States.bossState
{
    public class BosState
    {
        protected Boss5Control boss;
        protected EnemyAISO bossData;

        // Reference to the state machine for Minotaur 
        protected BossStateMachine stateMachine;

        public BosState(Boss5Control boss, BossStateMachine stateMachine)
        {
            this.boss = boss;
            this.stateMachine = stateMachine;
        }
        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
    
}