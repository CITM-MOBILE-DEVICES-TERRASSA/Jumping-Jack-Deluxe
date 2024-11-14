using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float slideDuration = 0.5f;
    public float slideSpeed = 5f;          // Por si hay que reducir la velocidad al deslizarse

    public Collider2D playerCollider;
    public Collider2D slideCollider;

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isSliding = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isSliding)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else rb.velocity = new Vector2(direction.x * slideSpeed, rb.velocity.y);
    }
    public void ChangeDirection()
    {
        direction = -direction;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    public void Slide()
    {
        if(isGrounded && !isSliding)
        {
            StartCoroutine(SlideCoroutine());
        }
    }

    IEnumerator SlideCoroutine()
    {
        isSliding = true;
        playerCollider.enabled = false;
        slideCollider.enabled = true;

        yield return new WaitForSeconds(slideDuration);

        playerCollider.enabled = true;
        slideCollider.enabled = false;
        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
