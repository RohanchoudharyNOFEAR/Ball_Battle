using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBallSawpnerMaze : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
          Vector3 sapwnpos = spawnPoints[ Random.Range(0, spawnPoints.Length)].transform.position;
          ball.transform.position = sapwnpos;
    }

    
}
