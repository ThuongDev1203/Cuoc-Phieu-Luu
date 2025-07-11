using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// RunState class for handling the run state of the player
    /// </summary>
    public class RunState : PlayerState
    {
        public RunState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.UpdateMovement(player.InputX, player.IsGrounded, player.Rigidbody.velocity.y);
        }

        public override void Update()
        {
            player.Anim.UpdateMovement(player.InputX, player.IsGrounded, player.Rigidbody.velocity.y);

            if (!player.IsGrounded)
            {
                player.ChangeState(new InAirState(player));
            }
            else if (player.JumpPressed)
            {
                player.SetJumpPressed();
                player.ChangeState(new InAirState(player));
            }
            else if (Mathf.Abs(player.InputX) < 0.1f)
            {
                player.ChangeState(new IdleState(player));
            }
        }
    }
}
