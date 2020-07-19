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

    Rigidbody2D rb;
    Animator anim;
    float damage = 1f;
    bool canMove = true;
    SpriteRenderer playerSprite;

    private float hAxis;
    private float vAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!canMove)
            return;

        hAxis = Input.GetAxisRaw("Horizontal") * damage;
        vAxis = Input.GetAxisRaw("Vertical") * damage;

        anim.SetInteger("Walk", (int)(Mathf.Abs(hAxis) + Mathf.Abs(vAxis)));
        rb.MovePosition((Vector2)transform.position + new Vector2(hAxis, vAxis) * playerSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Item"))
        {
            damage = 1f;
            playerSprite.color = Color.cyan;
            var vel = envirParticles.velocityOverLifetime;
            vel.xMultiplier = 1f;
        }

        if(collision.collider.CompareTag("Enemy"))
        {
            damage = -1f;
            playerSprite.color = Color.red;
            var vel = envirParticles.velocityOverLifetime;
            vel.xMultiplier = -1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            if(Vector3.Magnitude(collision.transform.position - transform.position) < 0.01f && Mathf.Abs(Quaternion.Angle(collision.transform.rotation, transform.rotation)) < 3f)
            {
                if (!goalParticles.isPlaying)
                {
                    goalParticles.Play();
                    camShake.shakeDuration = 0.5f;
                    canMove = false;
                    anim.SetInteger("Walk", 0);
                }

                rb.velocity = Vector2.zero;
                collision.GetComponent<SpriteRenderer>().color = Color.yellow;
                gameLoop.LevelWin();
            }
        }
    }
}
