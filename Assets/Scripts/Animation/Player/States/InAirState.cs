using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// InAirState handles both jumping and falling using blend tree
    /// </summary>
    public class InAirState : PlayerState
    {
        public InAirState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            // Reset jump input sau khi nhảy
            player.ResetJumpPressed();
        }

        public override void Update()
        {
            player.Anim.UpdateMovement(player.InputX, player.IsGrounded, player.Rigidbody.velocity.y);

            if (player.IsGrounded)
            {
                if (Mathf.Abs(player.InputX) > 0.1f)
                    player.ChangeState(new RunState(player));
                else
                    player.ChangeState(new IdleState(player));
            }
        }
    }
}
