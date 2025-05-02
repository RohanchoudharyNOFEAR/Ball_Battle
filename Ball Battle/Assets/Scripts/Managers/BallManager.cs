using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform playerField;
    public Transform enemyField;

    public bool isPlayerAttacking;

    private GameObject currentBall;

    public void SpawnBall(bool playerAttacks)
    {
        isPlayerAttacking = playerAttacks;

        Vector3 spawnPos = GetRandomPositionOnField(playerAttacks ? playerField : enemyField);
        currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetRandomPositionOnField(Transform field)
    {
        Renderer r = field.GetComponent<Renderer>();
        Vector3 center = r.bounds.center;
        Vector3 size = r.bounds.size;

        return new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            1.7f,
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
    }

    public GameObject GetBall() => currentBall;
}