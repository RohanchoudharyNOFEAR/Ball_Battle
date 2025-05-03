using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveState : ISoldierState
{
    private float delay;
    private float timer;

    public InactiveState(float delay)
    {
        this.delay = delay;
    }

    public void Enter(Soldier soldier)
    {
        soldier.SetColor(false);
        soldier.anim?.SetBool("Run",false);
        timer = delay;
    }

    public void Update(Soldier soldier)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            soldier.TransitionToState(soldier.GetInitialActiveState());
        }
    }

    public void Exit(Soldier soldier)
    {
        soldier.PlayEffect(soldier.reactivateEffect);
    }
}
