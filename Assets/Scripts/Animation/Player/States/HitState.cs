using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// HitState class for handling the hit state of the player
    /// </summary>
    public class HitState : PlayerState
    {
        public HitState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerHit();
        }

        public override void Update()
        {
            if (player.Anim.IsAnimationFinished())
            {
                if (player.IsGrounded)
                    player.ChangeState(new IdleState(player));
                else
                    player.ChangeState(new InAirState(player));
            }
        }

    }
}
