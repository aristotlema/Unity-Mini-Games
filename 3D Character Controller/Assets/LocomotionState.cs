using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionState : PlayerState
{
    public LocomotionState(Player player, PlayerStateMachine _stateMachine, string _animationName) : base(player, _stateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        player.HandleMovement();
        player.HandleAim();
    }
}
