using System.Security.Cryptography.X509Certificates;
using Animation.Boss.state;
using Annimation.Boss.Manager;
using UnityEngine;
namespace Animation.State.Boss.Run{
public class RunBoss5 : BossState, IEnemyState
{
    private readonly Boss5Control _bossControl;

    public RunBoss5(Boss5Control boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        _bossControl = boss;
    }

        public void EnterState()
        {
            base.Enter();
            _bossControl._animator.SetBool("isRunning", true);
            Debug.Log("Boss is now running.");
    }

    public void ExitState()
    {
        base.Exit();
        _bossControl._animator.SetBool("isRunning", false);
    }

        public void UpdateState()
        {
            base.LogicUpdate();
         }
}
}