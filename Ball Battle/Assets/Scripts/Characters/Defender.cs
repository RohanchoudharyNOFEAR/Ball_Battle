using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Soldier
{
    public float detectionRange = 5f;
    public float chaseSpeed = 1f;
    public float returnSpeed = 2f;
    public Vector3 startPos;

    public override void Start()
    {
        base.Start();

        if (gameManager.currentTurn == GameManager.Turn.PlayerAttack)
        {
            originalColor = Color.black;
            rend.material.color = originalColor;
        }

        startPos = transform.position;
    }

    public override ISoldierState GetInitialActiveState()
    {
        return new DefenderIdleState();
    }
}
