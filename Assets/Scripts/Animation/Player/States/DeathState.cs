using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// DeathState class for handling the death state of the player
    /// </summary>
    public class DeathState : PlayerState
    {
        public DeathState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerDeath();
            // Không chuyển nữa
        }
    }

}
