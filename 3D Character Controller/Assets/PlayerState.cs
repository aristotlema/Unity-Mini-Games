using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected float xInput;
    protected float yInput;
    protected string animationName;

    protected float stateTimer;

    public PlayerState(Player player, PlayerStateMachine _stateMachine, string _animationName)
    {
        this.player = player;
        this.stateMachine = _stateMachine;
        this.animationName = _animationName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animationName, true);
    }

    public virtual void Update()
    {
        player.HandleMovement();
    }
    public virtual void Exit()
    {
        player.animator.SetBool(animationName, false);
    }
}
