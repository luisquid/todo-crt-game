using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    Rigidbody2D rb;
    Animator anim;
    float damage = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
}
