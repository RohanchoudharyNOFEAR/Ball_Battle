using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ResultScreen : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text resultText;

    public void ShowResult(string result)
    {
        panel.SetActive(true);
        resultText.text = result;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // your menu scene name
    }
}