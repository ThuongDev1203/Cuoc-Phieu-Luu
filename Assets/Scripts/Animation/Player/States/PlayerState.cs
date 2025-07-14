using Animation.Player.Controller;

namespace Animation.Player.States
{
    /// <summary>
    /// Base class for player states
    /// </summary>
    public abstract class PlayerState
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
