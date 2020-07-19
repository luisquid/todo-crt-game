using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public ParticleSystem goalParticles;
    public ParticleSystem envirParticles; 
    public CameraShake camShake;
    public GameLoop gameLoop;
    public bool canMove = true;
    public int playerStatus = 0;

    [Header("Audio Clips")]
    public AudioClip hitClip;
    public AudioClip pickupClip;
    public AudioClip powerDownClip;
    public AudioClip winHitClip;

    Rigidbody2D rb;
    Animator anim;
    AudioSource audioPlayer;
    float damage = 1f;
    SpriteRenderer playerSprite;
    bool canWin = false;
    bool isWinning = false;

    [Header("Player Axis")]
    public float hAxis;
    public float vAxis;

    public void ResetPlayer()
    {
        damage = 1f;
        playerStatus = 0;
        canMove = true;
        isWinning = false;
        rb.velocity = Vector2.zero;
        playerSprite.color = Color.white;
        transform.position = transform.parent.position;
        transform.rotation = Quaternion.identity;
        anim.SetBool("Win", false);
    }
    
    public void MoveThing()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(hAxis, vAxis) * playerSpeed * Time.fixedDeltaTime);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();

        Color goalColor = Color.white;

        switch (playerStatus)
        {
            case (int)WinCondition.neutral:
                break;
            case (int)WinCondition.red:
                goalColor = Color.red;
                break;
            case (int)WinCondition.blue:
                goalColor = Color.cyan;
                break;
        }
        playerSprite.color = goalColor;
    }

    void Update()
    {
        if (!canMove)
            return;

        hAxis = Input.GetAxisRaw("Horizontal") * damage;
        vAxis = Input.GetAxisRaw("Vertical") * damage;

        anim.SetInteger("Walk", (int)(Mathf.Abs(hAxis) + Mathf.Abs(vAxis)));
        MoveThing();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Item"))
        {
            damage = 1f;
            playerSprite.color = Color.cyan;
            var vel = envirParticles.velocityOverLifetime;
            vel.xMultiplier = 1f;
            playerStatus = 1;
            audioPlayer.clip = pickupClip;
        }

        else if(collision.collider.CompareTag("Enemy"))
        {
            damage = -1f;
            playerSprite.color = Color.red;
            var vel = envirParticles.velocityOverLifetime;
            vel.xMultiplier = -1f;
            playerStatus = 2;
            audioPlayer.clip = powerDownClip;
        }

        else
        {
            audioPlayer.clip = hitClip;
        }

        audioPlayer.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isWinning && collision.CompareTag("Goal"))
        {            
            if( Vector3.Magnitude(collision.transform.position - transform.position) < 0.02f && 
                Mathf.Abs(Quaternion.Angle(collision.transform.rotation, transform.rotation)) < 4f)
            {
                int condition = (int)collision.GetComponent<GoalConditions>().winSetup;
                canWin = false;
                switch (condition)
                {
                    case (int)WinCondition.neutral:
                            canWin = true;
                        break;

                    case (int)WinCondition.red:
                        if (playerStatus == condition)
                            canWin = true;
                        break;

                    case (int)WinCondition.blue:
                        if (playerStatus == condition)
                            canWin = true;
                        break;
                }

                if (canWin)
                {
                    audioPlayer.clip = winHitClip;
                    audioPlayer.Play();


                    isWinning = true;

                    if (!goalParticles.isPlaying)
                    {
                        goalParticles.Play();
                        camShake.shakeDuration = 0.5f;
                        anim.SetInteger("Walk", 0);
                    }

                    transform.position = collision.transform.position;
                    canMove = false;

                    rb.velocity = Vector2.zero;

                    collision.GetComponent<SpriteRenderer>().color = Color.yellow;
                    collision.GetComponent<Animator>().SetBool("Win", true);
                    anim.SetBool("Win", true);
                    playerSprite.color = Color.yellow;

                    gameLoop.LevelWin();
                }        
            }
        }
    }
}
