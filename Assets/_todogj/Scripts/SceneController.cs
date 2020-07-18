using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private readonly string environmentSceneIndex = "Environment";

    public bool loadTest = false;

    #region TEST
    private void Start()
    {
        if (loadTest)
        {
            StartCoroutine(IE_LoadTest());
        }
    }

    private IEnumerator IE_LoadTest()
    {
        yield return new WaitForSeconds(15);
        LoadEnvironment();
    }
    
    #endregion
   

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
