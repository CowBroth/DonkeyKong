//using UnityEditor.ShaderGraph;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerScript player, StateMachine sm) : base(player, sm){}
    Vector2 swipeStartPos;
    float swipeTimeStart;
    public override void PhysicsUpdate()
    {
        player.PlayerMove();
        if (player.controls.Movement.TouchBool.IsPressed() && swipeTimeStart == 0)
        {
            swipeStartPos = player.touchPosition;
            swipeTimeStart = Time.time;
        }
        if (!player.controls.Movement.TouchBool.IsPressed() && swipeTimeStart != 0)
        {
            float swipeTime = Time.time - swipeTimeStart;
            Vector2 swipeEndPos = player.touchPosition - swipeStartPos;

            if ((swipeEndPos.y > 120f) && Mathf.Abs(swipeEndPos.y) > Mathf.Abs(swipeStartPos.y))
            {
                player.PlayerJump();
            }
            
            swipeTimeStart = 0f;
        }
        //check for player jumping
        if ( player.jumpInput > 0 )
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
