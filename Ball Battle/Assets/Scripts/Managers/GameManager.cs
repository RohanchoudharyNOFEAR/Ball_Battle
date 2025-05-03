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

    private int matchCount = 0;
    private float matchTimer;
  [SerializeField]  private int PlayerWins = 0;
  [SerializeField]  private int EnemyWins = 0;

    public SoldierSpawner soldierSpawner;
    public BallManager ballManager;
    
    public bool isRushTime = false;
    public ResultScreen resultScreen;

    public float GetRemainingTime() => matchTimer;
    public string GetCurrentTurnText() => currentTurn == Turn.PlayerAttack ? "Attacking" : "Defending";

    private bool playerScored = false;
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
            StartCoroutine(EndMatch());
        }

        if (matchCount == 5)
        {
            if (PlayerWins > EnemyWins)
            {
                //player wins;
            }
            else if (PlayerWins > EnemyWins)
            {
                //enemy wins;
            }
            else
            {

            }
        }

        
    }

    public void OnGoalScored(bool playerScored)
    {
        this.playerScored = playerScored;
        if (playerScored)
        {
            PlayerWins++;
        }
        else
        {
            EnemyWins++;
        }
        Debug.Log(playerScored ? "PLAYER SCORED!" : "ENEMY SCORED!");
       StartCoroutine( EndMatch()); // Or handle win tracking
    }

    void StartNewMatch(Turn turn)
    {
        matchCount++;
        currentTurn = turn;
        GoalGateManager.Instance.setGatesTag();
        matchTimer = matchDuration;
        soldierSpawner.SetTurn(turn == Turn.PlayerAttack ? SoldierSpawner.TurnType.Attacker : SoldierSpawner.TurnType.Defender);

        // Ball spawn logic
        bool playerAttacking = (turn == Turn.PlayerAttack);
        ballManager.SpawnBall(playerAttacking);
    }



    IEnumerator EndMatch()
    {

        if ( resultScreen.gameObject.activeInHierarchy ==false )
        {            
            resultScreen.gameObject.SetActive(true);
        }
        if (playerScored)
            resultScreen.ShowResult("You Scores!");
        else if (!playerScored)
            resultScreen.ShowResult("Enemy Scores!");
        else
            resultScreen.ShowResult("Draw!");

        yield return new WaitForSeconds(3f);

        if (resultScreen.gameObject.activeInHierarchy == true)
        {
            resultScreen.gameObject.SetActive(false);
        }

        // You can add win/loss condition checks here
        Turn nextTurn = currentTurn == Turn.PlayerAttack ? Turn.PlayerDefense : Turn.PlayerAttack;
        if (matchCount <= 5)
        {
            ResetMatch();
            StartNewMatch(nextTurn);
        }
        
    }

    private void ResetMatch()
    {
        Destroy(ballManager.GetBall());
       
        Soldier[] soldiers = GameObject.FindObjectsOfType<Soldier>();
        foreach (Soldier s in soldiers)
        {
            Destroy(s.gameObject);

        }
    }
    void OnRushTimeStarted()
    {
        EnergySystem.Instance.SetRushTime(true);
    }


}
