using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;
using Manager;

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
            SoundManager.Instance.PLayLose();
            player.Anim.TriggerDeath();
            // Không chuyển nữa
        }
    }

}
