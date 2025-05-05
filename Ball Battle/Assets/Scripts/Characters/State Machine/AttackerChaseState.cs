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
        if (a.HasBall)
        {
            s.anim?.SetBool("Run", false);
            s.anim?.SetBool("Dribble", true);
           // s.anim?.SetBool("Run", false);
            float moveSpeed =  GameManager.Instance.IsRushTime ? a.GetChaseSpeed : a.GetCarrySpeed;
            if (a.GetEnemyGate != null)
            {
                Vector3 dirToGate = (a.GetEnemyGate.position - a.transform.position).normalized;
                dirToGate.y = 0;
                dirToGate = dirToGate.normalized;
                a.transform.forward = dirToGate;
               
                a.transform.position += dirToGate * moveSpeed * Time.deltaTime;
            }
           // a.transform.position += a.transform.forward * moveSpeed * Time.deltaTime;
        }
        else if (a.GetBall != null && a.GetBall.transform.parent == null)
        {
            // Ball is free
            Vector3 dir = (a.GetBall.transform.position - a.transform.position).normalized;
            a.transform.forward = dir;
            a.transform.position += dir * a.GetChaseSpeed * Time.deltaTime;
           
            float ballcatchthreeshold = a.transform.name == "Attacker_AR(Clone)" ? 0.05f : 0.15f;
            if (Vector3.Distance(a.transform.position, a.GetBall.transform.position) < ballcatchthreeshold)
            {
                a.PickUpBall();
            }
        }
        else
        {
            // Ball is held by someone else
            if (a.GetEnemyGate != null)
            {
                Vector3 dir = (a.GetEnemyGate.position - a.transform.position).normalized;
                a.transform.forward = dir;
                a.transform.position += dir * a.GetChaseSpeed * Time.deltaTime;
            }
        }
    }

    public void Exit(Soldier s) { }
}
