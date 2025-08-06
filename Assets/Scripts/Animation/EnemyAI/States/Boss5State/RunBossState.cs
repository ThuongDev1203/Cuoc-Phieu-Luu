using Animation.Boss5State.States;
using Animation.Boss5State.States.bossState;
using Annimation.Boss.Manager;
using UnityEngine;
namespace Animation.State.Boss.Run{
public class RunBossState : BosState, IEnemyState
{
    private readonly Boss5Control _bossControl;

    public RunBossState(Boss5Control boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        this._bossControl = boss;
    }

    public void EnterState()
    {
        base.Enter();
        _bossControl._animator.SetBool("isRunning", true);
    }

    public void ExitState()
    {
        base.Exit();
        _bossControl._animator.SetBool("isRunning", false);
    }

    public void UpdateState()
    {
        base.LogicUpdate();
        Transform target = _bossControl.GetCurrentTarget();
        Vector2 direction = target.position - _bossControl.transform.position;

        _bossControl.Flip(direction);
        _bossControl.MoveTowards(target.position);

        if (Vector2.Distance(_bossControl.transform.position, target.position) < 0.1f)
        {
            _bossControl.SwitchTarget();
        }
    }
}
}