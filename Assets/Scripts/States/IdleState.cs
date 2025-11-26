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
        if (player.inputAxis >= 0.5f)
        {
            player.sprite.flipX = false;
            player.isMoving = true;
        }
        else if (player.inputAxis <= -0.5f)
        {
            player.sprite.flipX = true;
            player.isMoving = true;
        }
        else
        {
            player.isMoving = false;
        }
    }
}
