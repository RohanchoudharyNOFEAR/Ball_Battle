using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Maze : MonoBehaviour
{

    public int GetTimer { get { return timer; } }

    [SerializeField] private ResultScreen rs;
    [SerializeField] private int timer = 50;

    private Coroutine timerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        timerCoroutine = StartCoroutine(updateTimer());
        //rs = GameObject.FindAnyObjectByType<ResultScreen>();
    }

    // Update is called once per frame


    public void PlayerWin()
    {
        if (timer > 0)
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine); 
            }
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU WIN", false);

        }
    }

    public void PlayerLose()
    {
        if (timer <= 0)
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU LOSE", false);
        }
    }

    IEnumerator updateTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);

            timer--;
           
        }
        PlayerLose();
    }

}
