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
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.OnGoalScored(isEnemyGoal);
                }

            }

            GameManager_Maze gameManager_Maze = GameObject.FindObjectOfType<GameManager_Maze>();
            
            if (gameManager_Maze)
            {
               
                Atacker_Maze atk_mz = other.GetComponent<Atacker_Maze>();
                if (atk_mz.HasBall)
                {
                    gameManager_Maze.PlayerWin();
                }
            }
        }
    }
}
