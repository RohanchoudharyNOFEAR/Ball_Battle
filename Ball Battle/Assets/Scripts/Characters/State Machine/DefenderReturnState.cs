using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DefenderReturnState : ISoldierState
{
    public void Enter(Soldier s)
    {
        s.SetColor(false);
       // s.anim?.SetTrigger("Reactivate");
    }

    public void Update(Soldier s)
    {
        var d = s as Defender;
        d.transform.position = Vector3.MoveTowards(d.transform.position, d.StartPos, d.ReturnSpeed * Time.deltaTime);
        d.transform.LookAt(d.StartPos);
        if (Vector3.Distance(d.transform.position, d.StartPos) < 0.1f)
        {
            if (GameManager.Instance.isRushTime)
                d.TransitionToState(new DefenderIdleState());
            else
                d.TransitionToState(new InactiveState(4f));
        }
    }

    public void Exit(Soldier s) { }
}