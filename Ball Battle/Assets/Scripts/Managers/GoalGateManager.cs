using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGateManager : MonoBehaviour
{
    public static GoalGateManager Instance;

    private GameManager gameManager;
    private bool goalgateinitalized = false;
    [SerializeField] private GoalZone Gate1;
    [SerializeField] private GoalZone Gate2;
    [SerializeField] private FenceZone Fence1;
    [SerializeField] private FenceZone Fence2;


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
        if (InitializationManager.instance.initialized && goalgateinitalized == false)
        {
            Initialize();
            goalgateinitalized = true;
        }
    }

    private void Initialize()
    {
        Gate1 = GameObject.Find("Gate1").GetComponent<GoalZone>();
        Gate2 = GameObject.Find("Gate2").GetComponent<GoalZone>();
        Fence1 = GameObject.Find("Fence1").GetComponent<FenceZone>();
        Fence2 = GameObject.Find("Fence2").GetComponent<FenceZone>();
    }

    public void setGatesTag()
    {
        gameManager = GameManager.Instance;
        if (gameManager.currentTurn == GameManager.Turn.PlayerAttack)
        {
            Gate1.IsEnemyGoal = false;
            Gate2.IsEnemyGoal = true;
            Gate2.Renderer.material.color = Gate2.GoalColour;

            Fence1.isEnemyGoal = false;
            Fence2.isEnemyGoal = true;

            Gate1.tag = "PlayerGate";
            Gate2.tag = "EnemyGate";
        }
        else if (gameManager.currentTurn == GameManager.Turn.PlayerDefense)
        {
            Gate2.tag = "PlayerGate";
            Gate1.tag = "EnemyGate";
            // Gate1.GetComponent<GoalZone>().isEnemyGoal = false;
            // Gate2.GetComponent<GoalZone>().isEnemyGoal = true;

            Fence2.isEnemyGoal = false;
            Fence1.isEnemyGoal = true;
        }
    }
}
