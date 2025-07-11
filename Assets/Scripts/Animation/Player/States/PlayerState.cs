using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// Base class for player states
    /// </summary>
    public abstract class PlayerState : MonoBehaviour
    {
        protected PlayerController player;

        public PlayerState(PlayerController player)
        {
            this.player = player;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}