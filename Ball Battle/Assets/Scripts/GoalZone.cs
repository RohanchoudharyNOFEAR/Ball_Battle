using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] private bool isEnemyGoal; // true = enemy gate = player wins if reached
    [SerializeField] private Color goalColour;
    [SerializeField] private Renderer renderer;
    [SerializeField] private GameObject ParticleEffect;
    public bool IsEnemyGoal {  get { return isEnemyGoal; } set { isEnemyGoal = value; } }
    public Color GoalColour => goalColour;
    public Renderer Renderer => renderer;

    

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
      
    }
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attacker"))
        {


            Attacker atk = other.GetComponent<Attacker>();
            if (atk != null && atk.HasBall)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.OnGoalScored(IsEnemyGoal);
                    VFXManager.Instance.PlayEffect(ParticleEffect, transform.position,3);
                    VFXManager.Instance.PlayEffect(ParticleEffect, transform.position *-0.5f, 3);
                }

            }

            GameManager_Maze gameManager_Maze = GameObject.FindObjectOfType<GameManager_Maze>();
            
            if (gameManager_Maze)
            {
               
                Atacker_Maze atk_mz = other.GetComponent<Atacker_Maze>();
                if (atk_mz.HasBall)
                {
                    gameManager_Maze.PlayerWin();
                    VFXManager.Instance.PlayEffect(ParticleEffect, transform.position, 3);
                    VFXManager.Instance.PlayEffect(ParticleEffect, transform.position*-0.5f, 3);
                }
            }
        }
    }
}
