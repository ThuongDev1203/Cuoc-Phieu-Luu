using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// ShowOffState class for handling the show-off state of the player
    /// </summary>  
    public class ShowOffState : PlayerState
    {
        public ShowOffState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerShowOff();
        }

        public override void Update()
        {
            if (player.Anim.IsAnimationFinished())
            {
                player.ChangeState(new IdleState(player));
            }
        }
    }
}