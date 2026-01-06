using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerScript player, StateMachine sm) : base(player, sm){}
    Vector2 direction;
    Vector2 startPosition;
    public override void PhysicsUpdate()
    {
        player.PlayerMove();
        //JumpSwipe();

        /*if (player.controls.Movement.TouchBool.IsPressed() && swipeTimeStart == 0)
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
        }*/

        //check for player jumping
        if (player.jumpInput > 0)
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
    /*public void JumpSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began: //Start of touch
                    startPosition = touch.position;
                    break;

                case TouchPhase.Moved: //Movement of current touch
                    direction = touch.position - startPosition;
                    break;

                case TouchPhase.Ended: //End of touch
                    
                    break;
            }
        }
    }*/
}
