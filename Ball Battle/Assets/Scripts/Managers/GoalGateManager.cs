using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGateManager : MonoBehaviour
{
    public static GoalGateManager Instance;
    private GameManager gameManager;
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject Fence1;
    public GameObject Fence2;

    private bool goalgateinitalized = false;
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
      if(InitializationManager.instance.initialized && goalgateinitalized==false)
        {
            Initialize();
            goalgateinitalized=true;
        }
    }

    private void Initialize()
    {
        Gate1 = GameObject.Find("Gate1");
        Gate2 = GameObject.Find("Gate2");
        Fence1 = GameObject.Find("Fence1");
        Fence2 = GameObject.Find("Fence2");
    }

    public void setGatesTag()
    {
        gameManager = GameManager.Instance;
        if (gameManager.currentTurn == GameManager.Turn.PlayerAttack)
        {
            Gate1.GetComponent<GoalZone>().isEnemyGoal = false;
            Gate2.GetComponent<GoalZone>().isEnemyGoal = true;

            Fence1.GetComponent<FenceZone>().isEnemyGoal = false;
            Fence2.GetComponent<FenceZone>().isEnemyGoal = true;

            Gate1.tag = "PlayerGate";
            Gate2.tag = "EnemyGate";
        }
        else if (gameManager.currentTurn == GameManager.Turn.PlayerDefense)
        {
            Gate2.tag = "PlayerGate";
            Gate1.tag = "EnemyGate";
            // Gate1.GetComponent<GoalZone>().isEnemyGoal = false;
            // Gate2.GetComponent<GoalZone>().isEnemyGoal = true;

            Fence2.GetComponent<FenceZone>().isEnemyGoal = false;
            Fence1.GetComponent<FenceZone>().isEnemyGoal = true;
        }
    }
}
