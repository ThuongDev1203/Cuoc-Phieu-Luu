using System.Collections;
using System.Collections.Generic;
using Animation.Boss.state;
using Annimation.Boss.Manager;
using UnityEngine;

public class IdleBoss5 : BossState, IEnemyState
{
    private readonly Boss5Control _bossControl;

    public IdleBoss5(Boss5Control boss, BossStateMachine stateMachine) : base(boss, stateMachine)
    {
        this._bossControl = boss;
    }
    public void EnterState()
    {
        base.Enter();
        _bossControl._animator.SetBool("isIdle", true);
        Debug.Log("Boss is now idle.");
    }

    public void ExitState()
    {
        base.Exit();
        _bossControl._animator.SetBool("isIdle", false);
        Debug.Log("Boss exited idle state.");
    }

    public void UpdateState()
    {

    }
}
