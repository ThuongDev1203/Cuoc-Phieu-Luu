using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// IdleState class for handling the idle state of the player
    /// </summary>
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.UpdateMovement(0f, player.IsGrounded, player.Rigidbody.velocity.y);
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
            else if (Mathf.Abs(player.InputX) > 0.1f)
            {
                player.ChangeState(new RunState(player));
            }
        }
    }
}
