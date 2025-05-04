using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MazeInGameUI : MonoBehaviour
{
    public TMP_Text timerText;

    [SerializeField]
    private GameManager_Maze gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm != null)
        {
            timerText.text = Mathf.CeilToInt(gm.GetTimer).ToString();
        }
    }
}
