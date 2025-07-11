using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// Attack2State class for handling the second attack state of the player
    /// </summary>  
    public class Attack2State : PlayerState
    {
        public Attack2State(PlayerController player) : base(player) { }

        public override void Enter()
        {
            player.Anim.TriggerAttack2();
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
