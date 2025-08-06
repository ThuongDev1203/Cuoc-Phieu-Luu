using System.Collections;
using System.Collections.Generic;
using Annimation.Boss.Manager;
using UnityEngine;
namespace Animation.Boss.state
{
    /// <summary>
    /// EnemyStateMachine class for managing enemy AI states
    /// </summary>
    public class BossState 
    {
        public Boss5Control boss;
        public BossStateMachine stateMachine;
        public BossState(Boss5Control boss, BossStateMachine stateMachine)
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