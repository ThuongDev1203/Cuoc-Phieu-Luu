using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// InjuredState class for handling the injured state of the player
    /// </summary>
    public class InjuredState : PlayerState
    {
        public InjuredState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerInjured();
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
