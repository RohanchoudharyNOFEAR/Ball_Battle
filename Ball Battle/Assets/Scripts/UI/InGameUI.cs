using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public TMP_Text timerText;
    public Slider energyBar;
    public TMP_Text turnText;

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

        energyBar.value = es.GetNormalizedEnergy();
    }
}
