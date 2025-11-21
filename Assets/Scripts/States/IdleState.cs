using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerScript player, StateMachine sm) : base(player, sm){}

    public override void PhysicsUpdate()
    {
        player.PlayerMove();

        //check for player jumping
        if( player.jumpInput > 0 )
        {
            Debug.Log("jump");
            player.PlayerJump();
        }
    }
}
