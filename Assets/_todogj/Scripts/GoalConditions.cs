using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WinCondition
{
    neutral,
    blue,
    red
}

public class GoalConditions : MonoBehaviour
{
    [SerializeField]
    public WinCondition winSetup;

    private void Start()
    {
        Color goalColor = Color.white;

        switch(winSetup)
        {
            case WinCondition.neutral:
                goalColor = Color.white;
                break;
            case WinCondition.red:
                goalColor = Color.red;
                break;
            case WinCondition.blue:
                goalColor = Color.cyan;
                break;
        }
        GetComponent<SpriteRenderer>().color = goalColor;
    }
}
