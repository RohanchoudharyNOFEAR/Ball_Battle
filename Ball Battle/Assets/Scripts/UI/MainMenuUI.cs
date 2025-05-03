using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject menuPanel;

    public void OnStartGame()
    {
        SceneManager.LoadScene("MainGameScene"); // Name of your game scene
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
