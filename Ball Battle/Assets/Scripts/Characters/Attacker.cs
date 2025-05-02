using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Soldier
{
    public GameObject ball;
    public bool hasBall = false;

    public float chaseSpeed = 1.5f;
    public float carrySpeed = 0.75f;

    public GameObject highlight;

    public override void Start()
    {
        base.Start();
       // ball = GameObject.FindObjectOfType<BallManager>().GetBall();
    }

    public override ISoldierState GetInitialActiveState()
    {
        return new AttackerChaseState();
    }

    public void PickUpBall()
    {
        hasBall = true;
        if (highlight) highlight.SetActive(true);
        ball.transform.SetParent(transform);
        ball.transform.localPosition = Vector3.forward;
        anim?.SetBool("HasBall", true);
    }

    public void DropBall()
    {
        hasBall = false;
        if (highlight) highlight.SetActive(false);
        ball.transform.SetParent(null);
        anim?.SetBool("HasBall", false);
        TransitionToState(new InactiveState(2.5f));
    }
}