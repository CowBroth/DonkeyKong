using UnityEngine;

public class ClimbingState : State
{
    public ClimbingState(PlayerScript player, StateMachine sm) : base(player, sm) { }

    public override void PhysicsUpdate()
    {
        player.Climbing();
    }
}
