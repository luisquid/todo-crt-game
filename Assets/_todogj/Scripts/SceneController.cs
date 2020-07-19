using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private readonly string environmentSceneIndex = "Environment";
    
    private readonly int gameSceneIndex = 0;


    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
    
   

    public void LoadEnvironment()
    {
        StartCoroutine(IE_LoadAdditive(environmentSceneIndex));
    }
    
    private IEnumerator IE_LoadAdditive(string sceneIndex)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!asyncOp.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene( SceneManager.GetSceneByName( environmentSceneIndex ) );
        Debug.Log("Scene loaded!");
        
    }
}
