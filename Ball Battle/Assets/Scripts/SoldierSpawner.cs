using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject attackerPrefab;
    public GameObject defenderPrefab;
    public LayerMask landFieldLayer;
    public float rayLength = 100f;

    public enum TurnType { Attacker, Defender }
    public TurnType currentTurn;

    public EnergySystem energySystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTap();
        }
    }

    void HandleTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, landFieldLayer))
        {
            string tag = hit.collider.tag;

            if (currentTurn == TurnType.Attacker && tag == "PlayerField")
            {
                if (energySystem.TryUseEnergy(2))
                {
                    SpawnSoldier(attackerPrefab, hit.point);
                }
            }
            else if (currentTurn == TurnType.Defender && tag == "PlayerField")
            {
                if (energySystem.TryUseEnergy(3))
                {
                    SpawnSoldier(defenderPrefab, hit.point);
                }
            }
            else if (currentTurn == TurnType.Attacker && tag == "EnemyField")
            {
                if (energySystem.TryUseEnemyEnergy(3))
                {
                    SpawnSoldier(defenderPrefab, hit.point);
                }
            }
            else if (currentTurn == TurnType.Defender && tag == "EnemyField")
            {
                if (energySystem.TryUseEnemyEnergy(2))
                {
                    SpawnSoldier(attackerPrefab, hit.point);
                }
            }
        }
    }

    void SpawnSoldier(GameObject prefab, Vector3 spawnPosition)
    {
        Vector3 adjustedPosition = new Vector3(spawnPosition.x, 1.5f, spawnPosition.z); // adjust height as needed
        Instantiate(prefab, adjustedPosition, Quaternion.identity);
    }

    public void SetTurn(TurnType newTurn)
    {
        currentTurn = newTurn;
    }
}
