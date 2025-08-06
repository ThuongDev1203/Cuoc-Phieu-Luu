using Animation.Boss5State.States;
using Annimation.Boss.Manager;
using Animation.Boss5State.States.bossState;
namespace Animation.State.Boss.Idle
{
    public class IdleBossState : BosState, IEnemyState
    {
       public IdleBossState(Boss5Control boss, BossStateMachine stateMachine) : base (boss, stateMachine)
        {
            this.boss = boss;
            this.stateMachine = stateMachine;
        }
        private readonly Boss5Control _bossControl;
        public void EnterState()
        {
            base.Enter();
           _bossControl._animator.SetBool("isIdle", true);
            
        }

        public void ExitState()
        {
            base.Exit();
            _bossControl._animator.SetBool("isIdle", false);
        }

        public void UpdateState()
        {
            base.LogicUpdate();
        }
    }
}