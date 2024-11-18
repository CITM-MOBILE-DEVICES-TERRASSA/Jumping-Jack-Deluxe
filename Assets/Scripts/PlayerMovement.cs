using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float slideDuration = 0.5f;
    public float slideSpeed = 5f;               // Por si hay que reducir la velocidad al deslizarse


    private Queue<Vector2> positionHistory = new Queue<Vector2>();
    public int positionCheckFrames = 20;
    public float positionTolerance = 0.5f; // Allowed position variation before considering it idle

    private float idleTimer = 0.0f;
    public float idleDuration = 3f;         // Tiempo que debe estar quieto para GameOver


    public Collider2D playerCollider;
    public Collider2D slideCollider;
    public Transform spriteTransform;           // Para hacer visual el slide

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isSliding = false;
    private int jumpCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        positionHistory.Enqueue(rb.position);

        if (positionHistory.Count > positionCheckFrames)
        {
            positionHistory.Dequeue();
        }

        if (HasBeenIdle())
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleDuration)
            {
                Debug.Log("DEAD ALREADY");
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            idleTimer = 0f;
        }

        if (!isSliding)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else rb.velocity = new Vector2(direction.x * slideSpeed, rb.velocity.y);
    }

    private bool HasBeenIdle()
    {
        if (positionHistory.Count < positionCheckFrames)
            return false;

        Vector2 firstPosition = positionHistory.Peek();
        foreach (Vector2 position in positionHistory)
        {
            if (Vector2.Distance(firstPosition, position) > positionTolerance)
            {
                return false;
            }
        }
        return true;
    }

    public void ChangeDirection()
    {
        direction = -direction;
    }

    public void Jump()
    {
        if (isGrounded || jumpCounter < 2)
        {
            Vector2 velocity = rb.velocity;
            velocity.y = 0; 
            rb.velocity = velocity; 

            jumpCounter++;
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

        spriteTransform.localScale = new Vector3(1f, 0.5f, 1f);     // Reducir visualmente el sprite
        
        yield return new WaitForSeconds(slideDuration);

        playerCollider.enabled = true;
        slideCollider.enabled = false;

        spriteTransform.localScale = Vector3.one;   // Restaurar el sprite
        isSliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCounter = 0;
        }
    }
}
