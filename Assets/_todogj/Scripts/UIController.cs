using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI LevelSideText;
    public TextMeshProUGUI LevelText;

    public GameObject GamePanel;
    public GameObject MenuPanel;
    public GameObject LevelWinPanel;
    public GameObject GameOverPanel;

    private int selfdestruct = 5;

    public void StartGame()
    {
        GamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        LevelWinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        LevelWinPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void LevelWin(int _levelIndex)
    {
        LevelText.text = "LEVEL " + (_levelIndex - 1)+ " WON!\npress space";
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        LevelWinPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void Oops() => StartCoroutine(SelfDestruct());
    IEnumerator SelfDestruct()
    {
        print("BIG OOPS");
        if (selfdestruct == 0)
        {
            Application.Quit();
            StopCoroutine(SelfDestruct());
            print("DEAD");
        }

        LevelWinPanel.SetActive(true);
        LevelText.text = "LEVELS NOT FOUND. SELF DESTRUCT IN " + selfdestruct;
        yield return new WaitForSeconds(1f);

        selfdestruct--;

        StartCoroutine(SelfDestruct());
    }

    public void UpdateTimer(float _time)
    {
        TimeSpan time = TimeSpan.FromSeconds(_time);
        TimerText.text = string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
    }
}
