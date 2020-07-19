using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public bool gameStarted = false;
    public float timer;
    public UIController uiController;
    public List<GameObject> levels;

    public int levelIndex = 0;

    public void StartGame()
    {
        uiController.StartGame();
        if (levelIndex >= levels.Count)
            uiController.Oops();
        else
        {
            levels[levelIndex].SetActive(true);
            gameStarted = true;
            timer = 30f;
        }   
    }

    public void LevelWin()
    {
        levelIndex++;

        uiController.LevelWin(levelIndex);
        gameStarted = false;

        if(levelIndex > levels.Count)
        {
            levelIndex = 0;
            GoToEnding();
        }
    }

    public void GameOver()
    {
        gameStarted = false;
        uiController.GameOver();
    }

    public void GoToEnding()
    {
        print("YOU FIGURED IT OUT");
    }

    // Start is called before the first frame update
    void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("LEVEL");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartGame();
        if (Input.GetKeyDown(KeyCode.X))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        if (!gameStarted)
            return;

        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            timer = 0f;
            GameOver();
        }

        uiController.UpdateTimer(timer);
    }
}
