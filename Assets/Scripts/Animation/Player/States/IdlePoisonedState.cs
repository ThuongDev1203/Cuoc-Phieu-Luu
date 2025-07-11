using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// IdlePoisonedState class for handling the idle poisoned state of the player
    /// </summary>
    public class IdlePoisonedState : PlayerState
    {
        public IdlePoisonedState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerIdlePoisoned();
        }

        public override void Update()
        {
            // Có thể tự chuyển lại Idle nếu cần
            if (player.Anim.IsAnimationFinished())
            {
                player.ChangeState(new IdleState(player));
            }
        }
    }
}
