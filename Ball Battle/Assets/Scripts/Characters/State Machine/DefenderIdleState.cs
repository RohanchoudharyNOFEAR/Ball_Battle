using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderIdleState : ISoldierState
{
    public void Enter(Soldier s)
    {
        s.SetColor(true);
        s.PlayEffect(s.activationEffect);
        var d = s as Defender;
        d.DefendCircleHighLight.SetActive(true);
    }

    public void Update(Soldier s)
    {
        var d = s as Defender;
        Attacker[] attackers = GameObject.FindObjectsOfType<Attacker>();

        foreach (var atk in attackers)
        {
            if (atk.HasBall && atk.GetCurrentState is AttackerChaseState)
            {
                float dist = Vector3.Distance(d.transform.position, atk.transform.position);
                if (dist <= d.DetectionRange)
                {
                   
                    d.TransitionToState(new DefenderChaseState(atk));
                    return;
                }
            }
        }
    }

    public void Exit(Soldier s) { var d = s as Defender; d.DefendCircleHighLight.SetActive(false); }
}
