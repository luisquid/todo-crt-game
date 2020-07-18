using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public ParticleSystem goalParticles;
    public CameraShake camShake;

    Rigidbody2D rb;
    Animator anim;
    float damage = 1f;
    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            canMove = true;
        if (!canMove)
            return;

        float h = Input.GetAxisRaw("Horizontal") * damage;
        float v = Input.GetAxisRaw("Vertical") * damage;

        anim.SetInteger("Walk", (int)(Mathf.Abs(h) + Mathf.Abs(v)));

        rb.MovePosition((Vector2)transform.position + new Vector2(h, v) * playerSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Item"))
        {
            print("Im a general, weeeeee");
            damage = 1f;
        }

        if(collision.collider.CompareTag("Enemy"))
        {
            print("I am not throwing away my shot");
            damage = -1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            print("He wrote the other FIFTY ONE");
            print("OUR BOUNDS ARE THIS CLOSE: " + (collision.bounds.center - GetComponent<BoxCollider2D>().bounds.center));
            print("OUR BOUNDS ARE THIS CLOSE: " + (collision.transform.position - transform.position));

            if(Vector3.Magnitude(collision.transform.position - transform.position) < 0.01f)
            {
                if (!goalParticles.isPlaying)
                {
                    goalParticles.Play();
                    camShake.shakeDuration = 0.5f;
                    canMove = false;
                    anim.SetInteger("Walk", 0);
                }
            }
        }
    }
}
