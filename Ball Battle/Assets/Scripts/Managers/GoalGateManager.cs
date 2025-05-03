using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGateManager : MonoBehaviour
{
    public static GoalGateManager Instance;
    private GameManager gameManager;
    public GameObject Gate1;
    public GameObject Gate2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setGatesTag()
    {
        gameManager = GameManager.Instance;
        if (gameManager.currentTurn == GameManager.Turn.PlayerAttack)
        {
            Gate1.tag = "PlayerGate";
            Gate2.tag = "EnemyGate";
        }
        else if (gameManager.currentTurn == GameManager.Turn.PlayerDefense)
        {
            Gate2.tag = "PlayerGate";
            Gate1.tag = "EnemyGate";
        }
    }
}
