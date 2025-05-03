using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Soldier
{
    public GameObject ball;
    public Transform enemyGate;

    public bool hasBall = false;

    public float chaseSpeed = 1.5f;
    public float carrySpeed = 0.75f;

    public GameObject highlight;

    public override void Start()
    {
        base.Start();
        ball = GameObject.FindObjectOfType<BallManager>().GetBall();

        GameObject goalObj = GameObject.FindGameObjectWithTag("EnemyGate");
        if (goalObj)
            enemyGate = goalObj.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Fence") || other.CompareTag("EnemyGate")) && !hasBall)
        {
            Destroy(gameObject);
        }
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
        //anim?.SetBool("Run", false);
       // anim?.SetBool("Dribble", true);
    }

    public void DropBall()
    {
        hasBall = false;
        if (highlight) highlight.SetActive(false);
        ball.transform.SetParent(null);
        anim?.SetBool("Dribble", false);
       // anim?.SetBool("HasBall", false);
        TransitionToState(new InactiveState(2.5f));
    }

    public void PassBallTo(/*Attacker teammate*/)
    {

        Attacker[] teammates = GameObject.FindObjectsOfType<Attacker>();
        Attacker bestTarget = null;
        float mindistance = float.MaxValue;

        foreach (var teammate in teammates)
        {
            if (teammate != this)
            {
                float distToteamate = Vector3.Distance(teammate.transform.position, transform.position);
             
                if (distToteamate < mindistance)
                {
                    mindistance = distToteamate;
                    bestTarget = teammate;

                }
            }
        }


        if (!hasBall || bestTarget == null) return;

        DropBall();

        hasBall = false;
      //  highlight?.SetActive(false);
      //  ball.transform.SetParent(null);
     //   anim?.SetBool("Dribble", false);

        StartCoroutine(SmoothPass(bestTarget));
    }

    private IEnumerator SmoothPass(Attacker target)
    {
        float t = 0;
        float duration = 0.5f;
        Vector3 start = ball.transform.position;
        Vector3 end = target.transform.position + Vector3.forward;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            ball.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        target.PickUpBall();
    }
}