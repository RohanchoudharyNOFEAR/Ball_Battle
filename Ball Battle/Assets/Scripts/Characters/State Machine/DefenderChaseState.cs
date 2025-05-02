using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderChaseState : ISoldierState
{
    private Attacker target;

    public DefenderChaseState(Attacker target)
    {
        this.target = target;
    }

    public void Enter(Soldier s)
    {
        s.SetColor(true);
        s.PlayEffect(s.activationEffect);
        s.anim?.SetBool("Chasing", true);
    }

    public void Update(Soldier s)
    {
        var d = s as Defender;
        Vector3 dir = (target.transform.position - d.transform.position).normalized;
        d.transform.position += dir * d.chaseSpeed * Time.deltaTime;

        if (Vector3.Distance(d.transform.position, target.transform.position) < 0.5f)
        {
            target.DropBall();
            d.TransitionToState(new DefenderReturnState());
        }
    }

    public void Exit(Soldier s)
    {
        s.anim?.SetBool("Chasing", false);
    }
}