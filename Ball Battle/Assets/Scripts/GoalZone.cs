using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public bool isEnemyGoal; // true = enemy gate = player wins if reached

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attacker"))
        {
            Attacker atk = other.GetComponent<Attacker>();
            if (atk != null && atk.hasBall)
            {
                GameManager.Instance.OnGoalScored(isEnemyGoal);
            }
        }
    }
}
