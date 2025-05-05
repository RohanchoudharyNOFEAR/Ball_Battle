using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Soldier
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float chaseSpeed = 1f;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private GameObject defendCircleHighLight;

    public float DetectionRange => detectionRange;
    public float ChaseSpeed => chaseSpeed;
    public float ReturnSpeed => returnSpeed;
    public Vector3 StartPos => startPos;
    public GameObject DefendCircleHighLight => defendCircleHighLight;

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
