using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderChaseState : ISoldierState
{
    private Attacker target;
    public static Action CaughtAttackerEvent;
    public DefenderChaseState(Attacker target)
    {
        this.target = target;
    }

    public void Enter(Soldier s)
    {
        s.SetColor(true);
        s.PlayEffect(s.activationEffect);
        s.anim?.SetBool("Run", true);
    }

    public void Update(Soldier s)
    {
        var d = s as Defender;
        if (target == null) { d.TransitionToState(new DefenderReturnState()); return; }


        Vector3 dir = (target.transform.position - d.transform.position).normalized;
        d.transform.position += dir * d.ChaseSpeed * Time.deltaTime;
        d.transform.LookAt(target.transform.position);
        float attackercatchthreeshold = d.transform.name == "Defender_AR(Clone)" ? 0.0519f : 3.5f;
        if (Vector3.Distance(d.transform.position, target.transform.position) < attackercatchthreeshold)
        {
            if (target.GetComponent<Attacker>().HasBall)
            {
                //  d.transform.LookAt(target.transform.position);
                s.anim?.SetTrigger("Touch");
                if (CaughtAttackerEvent != null)
                {
                    CaughtAttackerEvent.Invoke();
                }
                target.PassBallTo();
            }
            d.TransitionToState(new DefenderReturnState());
        }
    }

    public void Exit(Soldier s)
    {
        s.anim?.SetBool("Run", false);
    }
}