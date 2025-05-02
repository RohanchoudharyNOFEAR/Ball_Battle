using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerChaseState : ISoldierState
{
    public void Enter(Soldier s)
    {
        s.SetColor(true);
        s.PlayEffect(s.activationEffect);
    }

    public void Update(Soldier s)
    {
        var a = s as Attacker;
        if (a.hasBall)
        {
            float moveSpeed =  GameManager.Instance.isRushTime ? a.chaseSpeed : a.carrySpeed;
            a.transform.position += a.transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (a.ball != null)
        {
            Vector3 dir = (a.ball.transform.position - a.transform.position).normalized;
            a.transform.forward = dir;
            a.transform.position += dir * a.chaseSpeed * Time.deltaTime;

            if (Vector3.Distance(a.transform.position, a.ball.transform.position) < 0.5f)
                a.PickUpBall();
        }
    }

    public void Exit(Soldier s) { }
}
