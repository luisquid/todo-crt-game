using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        anim.SetInteger("Walk", (int)(Mathf.Abs(h) + Mathf.Abs(v)));

        rb.MovePosition((Vector2)transform.position + new Vector2(h, v) * playerSpeed * Time.deltaTime);
    }
}
