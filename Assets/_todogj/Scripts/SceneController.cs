using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static string environmentSceneIndex = "Environment";
    
    private readonly int gameSceneIndex = 0;


    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }
    
    
    public static IEnumerator IE_LoadEnvironment()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(environmentSceneIndex, LoadSceneMode.Additive);

        while (!asyncOp.isDone)
        {
            yield return null;
        }
        
        SceneManager.SetActiveScene( SceneManager.GetSceneByName( environmentSceneIndex ) );
        Debug.Log("Scene loaded!");
        
    }
}
