using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Maze : MonoBehaviour
{
    [SerializeField]
    private int timer = 50;
    public int GetTimer {  get { return timer; } }
   [SerializeField] private ResultScreen rs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateTimer());
        //rs = GameObject.FindAnyObjectByType<ResultScreen>();
    }

    // Update is called once per frame
   

    public void PlayerWin()
    {
        if (timer > 0)
        {
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU WIN");

        }
    }

    public void PlayerLose()
    {
        if (timer <= 0)
        {
            StopCoroutine(updateTimer());
            rs.gameObject.SetActive(true);
            rs.ShowResult("YOU LOSE");
        }
    }

    IEnumerator updateTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);

            timer--;
            PlayerLose();
        }
    }

}
