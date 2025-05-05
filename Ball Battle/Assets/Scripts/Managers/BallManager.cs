using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform playerField;
    [SerializeField] private Transform enemyField;
    [SerializeField] private float spawnYPos = 1.4f;
    [SerializeField] private bool isPlayerAttacking;

    private GameObject currentBall;
    private bool ballManagerInitalized = false;

    public GameObject BallPrefab => ballPrefab;
    public Transform PlayerField => playerField;
    public Transform EnemyField => enemyField;
    public float SpawnYPos => spawnYPos;
    public bool IsPlayerAttacking => isPlayerAttacking;

   

    private void Update()
    {
        if (InitializationManager.instance.GetInitilized && ballManagerInitalized == false)
        {
            Initialize();
            ballManagerInitalized = true;
        }
    }

    private void Initialize()
    {
        playerField = GameObject.Find("PlayerField").transform;
        enemyField = GameObject.Find("EnemyField").transform;
    }

    public void SpawnBall(bool playerAttacks)
    {
        isPlayerAttacking = playerAttacks;

        Vector3 spawnPos = GetRandomPositionOnField(playerAttacks ? PlayerField : EnemyField);
        currentBall = Instantiate(BallPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetRandomPositionOnField(Transform field)
    {
        Renderer r = field.GetComponent<Renderer>();
        Vector3 center = r.bounds.center;
        Vector3 size = r.bounds.size;
       
        return new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            SpawnYPos,
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
    }

    public GameObject GetBall() => currentBall;
}