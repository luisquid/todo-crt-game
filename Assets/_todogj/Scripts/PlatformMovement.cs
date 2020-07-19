using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    WinCondition winSet;

    Transform playerTransform;
    PlayerMovement pMove;
    Rigidbody2D rb;

    Vector2 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;    
    }

    private void OnEnable()
    {
        transform.position = initialPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        pMove = playerTransform.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        Color goalColor = Color.white;

        switch (winSet)
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

    // Update is called once per frame
    void Update()
    {
        if(pMove.playerStatus == (int)winSet && winSet != WinCondition.neutral)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition((Vector2)transform.position + new Vector2(pMove.hAxis, pMove.vAxis) * pMove.playerSpeed * Time.fixedDeltaTime);
        }
    }
}
