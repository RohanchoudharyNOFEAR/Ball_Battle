using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceZone : MonoBehaviour
{
    public bool isEnemyGoal; // true = enemy gate = player wins if reached

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attacker") && isEnemyGoal)
        {
            Debug.Log("destroy attacker " + isEnemyGoal);
            Destroy(other.gameObject);

            
        }
    }
}
