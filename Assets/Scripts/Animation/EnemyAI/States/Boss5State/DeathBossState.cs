using Animation.Boss5State.States;
using Animation.Boss5State.States.bossState;
using Annimation.Boss.Manager;

namespace Animation.State.DeathBoss
{
    public class DeathBossState : BosState, IEnemyState
    {
       public DeathBossState(Boss5Control boss, BossStateMachine stateMachine) : base (boss, stateMachine)
        {
            this.boss = boss;
            this.stateMachine = stateMachine;
        }
        private readonly Boss5Control _bossControl;
        public void EnterState()
        {
            base.Enter();
           _bossControl._animator.SetTrigger("hitBoss");
            
        }

        public void ExitState()
        {
            base.Exit();
        }

        public void UpdateState()
        {
            base.LogicUpdate();
        }
    }
}