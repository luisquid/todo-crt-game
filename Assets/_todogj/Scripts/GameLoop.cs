using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameLoop : MonoBehaviour
{
    public bool gameStarted = false;
    public bool eraseProgress = false;
    public float timer;
    public Transform playerTransform;
    public DamageCue cueController;
    public UIController uiController;
    public List<GameObject> levels;
    public int levelIndex = 0;

    public bool isWinning = false;

    [Header("AUDIO")]
    public AudioSource[] audioSources;
    public AudioClip winClip;
    public AudioClip gameOverClip;
    public AudioMixerSnapshot game;
    public AudioMixerSnapshot gameOver;
    public void ClearLevels()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            levels[i].SetActive(false);
        }
    }

    public void StartGame()
    {
        uiController.StartGame();
        isWinning = false;
        
        if (levelIndex >= levels.Count)
        {
            //uiController.Oops();
            GoToEnding();
        }
        else
        {
            ClearLevels();
            playerTransform.GetComponent<PlayerMovement>().ResetPlayer();
            levels[levelIndex].SetActive(true);
            gameStarted = true;
            timer = levels[levelIndex].GetComponent<LevelSetup>().time;
            playerTransform.GetComponent<PlayerMovement>().envirParticles = GameObject.FindGameObjectWithTag("Env").GetComponent<ParticleSystem>();
        }
    }

    public void LevelWin()
    {
        if (isWinning)
            return;

        cueController.AddChromatic(1 / levels.Count);

        isWinning = true;
        audioSources[1].clip = winClip;
        audioSources[1].Play();

        levelIndex++;

        PlayerPrefs.SetInt("LEVEL", levelIndex);

        uiController.LevelWin(levelIndex);
        gameStarted = false;

        if(levelIndex > levels.Count)
        {
            print(levelIndex);
            GoToEnding();
        }
    }

    public void GameOver()
    {
        audioSources[1].clip = gameOverClip;
        audioSources[1].Play();
        gameStarted = false;
        cueController.GameOver();
        uiController.GameOver();
        playerTransform.GetComponent<PlayerMovement>().canMove = false;
    }

    public void GoToEnding()
    {
        print("YOU FIGURED IT OUT");
        PlayerPrefs.SetInt("LEVEL", 0);
        audioSources[0].volume = 0.4f;
        gameOver.TransitionTo(0.1f);
        cueController.AddDepth();
        StartCoroutine(SceneController.IE_LoadEnvironment());
    }

    // Start is called before the first frame update
    void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("LEVEL");
        
        if (eraseProgress)
        {
            levelIndex = 0;
            PlayerPrefs.SetInt("LEVEL", 0);
        }

        audioSources = GetComponents<AudioSource>();
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
