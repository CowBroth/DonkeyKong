using UnityEngine;

public abstract class State
{
    protected PlayerScript player;
    protected StateMachine sm;

    protected State(PlayerScript player, StateMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}
