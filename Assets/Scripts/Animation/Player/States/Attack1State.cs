using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// Attack1State class for handling the first attack state of the player
    /// </summary>
    public class Attack1State : PlayerState
    {
        public Attack1State(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerAttack1(); // Gọi trigger trong Animator
        }

        public override void Update()
        {
            if (player.Anim.IsAnimationFinished())
            {
                if (!player.IsGrounded)
                    player.ChangeState(new InAirState(player));
                else if (Mathf.Abs(player.InputX) > 0.1f)
                    player.ChangeState(new RunState(player));
                else
                    player.ChangeState(new IdleState(player));
            }
        }
    }
}
