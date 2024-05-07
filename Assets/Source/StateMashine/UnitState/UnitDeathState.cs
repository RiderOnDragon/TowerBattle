using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeathState : State
{
    private UnitAnimation _animation;

    public UnitDeathState(UnitAnimation unitAnimation)
    {
        _animation = unitAnimation;
    }

    public override void Enter()
    {
        _animation.StartDeath();
    }
}
