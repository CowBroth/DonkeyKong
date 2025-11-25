using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerScript player, StateMachine sm) : base(player, sm) { }
    public override void PhysicsUpdate()
    {
    }

    public override void Enter()
    {

    }

    public override void LogicUpdate()
    {
        //check for player landing
    }

}
