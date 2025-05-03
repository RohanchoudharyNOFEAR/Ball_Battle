using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoldierSpawner;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Turn { PlayerAttack, PlayerDefense }
    public Turn currentTurn;
    public float matchDuration = 140f;
    private float matchTimer;

    public SoldierSpawner soldierSpawner;
    public BallManager ballManager;
    public bool isRushTime = false;

    public float GetRemainingTime() => matchTimer;
    public string GetCurrentTurnText() => currentTurn == Turn.PlayerAttack ? "Attacking" : "Defending";

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
        StartNewMatch(Turn.PlayerAttack);
    }

    // Update is called once per frame
    void Update()
    {
        matchTimer -= Time.deltaTime;

       
        if (!isRushTime && matchTimer <= 15f)
        {
            isRushTime = true;
            Debug.Log("Rush Time Started!");
            OnRushTimeStarted();
        }

        if (matchTimer <= 0f)
        {
            EndMatch();
        }
    }

    public void OnGoalScored(bool playerScored)
    {
        Debug.Log(playerScored ? "PLAYER SCORED!" : "ENEMY SCORED!");
        EndMatch(); // Or handle win tracking
    }

    void StartNewMatch(Turn turn)
    {
        currentTurn = turn;
        matchTimer = matchDuration;
        soldierSpawner.SetTurn(turn == Turn.PlayerAttack ? SoldierSpawner.TurnType.Attacker : SoldierSpawner.TurnType.Defender);

        // Ball spawn logic
        bool playerAttacking = (turn == Turn.PlayerAttack);
        ballManager.SpawnBall(playerAttacking);
    }

   

    void EndMatch()
    {
        // You can add win/loss condition checks here
        Turn nextTurn = currentTurn == Turn.PlayerAttack ? Turn.PlayerDefense : Turn.PlayerAttack;
        StartNewMatch(nextTurn);
    }

    void OnRushTimeStarted()
    {
        EnergySystem.Instance.SetRushTime(true);
    }

   
}
