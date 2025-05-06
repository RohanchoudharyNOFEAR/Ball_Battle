using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text playerScoreText;
    [SerializeField] private TMP_Text enemyScoreText;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject MainMenuButton;

    public void ShowResult(string result,bool showScore = true)
    {
        panel.SetActive(true);
        resultText.text = result;
        if (showScore)
        {
            playerScoreText.text ="Player Score: " +GameManager.Instance.GetPlayerWins().ToString();
            enemyScoreText.text = "Enemy Score: " + GameManager.Instance.GetEnemyWins().ToString();
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); 
    }

    public void ShowButtons(bool show)
    {
        if (show)
        {
            playerScoreText.gameObject.SetActive(false);
            enemyScoreText.gameObject.SetActive(false);
            RestartButton.SetActive(true);
            MainMenuButton.SetActive(true);
        }
        else
        {
            MainMenuButton.SetActive(false);
            RestartButton.SetActive(false);
        }
    }
}