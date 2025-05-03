using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Maze : MonoBehaviour
{

    public int Timer = 50;

    public ResultScreen rs;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateTimer());
        //rs = GameObject.FindAnyObjectByType<ResultScreen>();
    }

    // Update is called once per frame
   

    public void PlayerWin()
    {
        if (Timer > 0)
        {
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU WIN");

        }
    }

    public void PlayerLose()
    {
        if (Timer <= 0)
        {
            StopCoroutine(updateTimer());
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU LOSE");
        }
    }

    IEnumerator updateTimer()
    {
        while (Timer > 0)
        {
            yield return new WaitForSeconds(1);

            Timer--;
            PlayerLose();
        }
    }

}
