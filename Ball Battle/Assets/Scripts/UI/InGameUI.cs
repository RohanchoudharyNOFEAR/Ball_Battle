using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public TMP_Text timerText;
    public Slider PlayerEnergyBar;
    public Slider EnemyEnergyBar;
    public TMP_Text turnText;
    public GameObject MainMenuButton;

    private GameManager gm;
    private EnergySystem es;

    void Update()
    {
        if (gm == null)
        {
            gm = GameManager.Instance;
            es = EnergySystem.Instance;
        }
        if (gm != null)
        {
            timerText.text = Mathf.CeilToInt(gm.GetRemainingTime()).ToString();
            turnText.text = gm.GetCurrentTurnText();
        }

        PlayerEnergyBar.value = es.GetPlayerNormalizedEnergy();
        EnemyEnergyBar.value = es.GetEnemyNormalizedEnergy();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
