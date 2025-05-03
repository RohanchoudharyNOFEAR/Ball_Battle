using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerChaseState : ISoldierState
{
    public void Enter(Soldier s)
    {
        s.SetColor(true);
        s.PlayEffect(s.activationEffect);
        s.anim?.SetBool("Run", true);
    }

    public void Update(Soldier s)
    {
        var a = s as Attacker;
        if (a.hasBall)
        {
            s.anim?.SetBool("Run", false);
            s.anim?.SetBool("Dribble", true);
           // s.anim?.SetBool("Run", false);
            float moveSpeed =  GameManager.Instance.isRushTime ? a.chaseSpeed : a.carrySpeed;
            if (a.enemyGate != null)
            {
                Vector3 dirToGate = (a.enemyGate.position - a.transform.position).normalized;
                dirToGate.y = 0;
                dirToGate = dirToGate.normalized;
                a.transform.forward = dirToGate;
               
                a.transform.position += dirToGate * moveSpeed * Time.deltaTime;
            }
           // a.transform.position += a.transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (a.ball != null && a.ball.transform.parent == null)
        {
            // Ball is free
            Vector3 dir = (a.ball.transform.position - a.transform.position).normalized;
            a.transform.forward = dir;
            a.transform.position += dir * a.chaseSpeed * Time.deltaTime;

            if (Vector3.Distance(a.transform.position, a.ball.transform.position) < 0.3f)
                a.PickUpBall();
        }
        else
        {
            // Ball is held by someone else
            if (a.enemyGate != null)
            {
                Vector3 dir = (a.enemyGate.position - a.transform.position).normalized;
                a.transform.forward = dir;
                a.transform.position += dir * a.chaseSpeed * Time.deltaTime;
            }
        }
    }

    public void Exit(Soldier s) { }
}
