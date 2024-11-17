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

    public float velocityThreshold = 0.01f;     // Umbral para considerar velocidad como cero
    public float idleDuration = 0.1f;           // Tiempo que debe estar quieto para GameOver


    public Collider2D playerCollider;
    public Collider2D slideCollider;
    public Transform spriteTransform;           // Para hacer visual el slide

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isSliding = false;
    private int jumpCounter = 0;
    private float idleTimer = 0f;               // Contador para el tiempo en reposo


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Comprueba si la velocidad está por debajo del umbral
        bool isIdle = Mathf.Abs(rb.velocity.x) < velocityThreshold && Mathf.Abs(rb.velocity.y) < velocityThreshold;

        // Si está quieto, aumenta el contador
        if (isIdle)
        {
            idleTimer += Time.deltaTime;

            // Si el contador supera el tiempo permitido, cambia a GameOver
            if (idleTimer >= idleDuration)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else
        {
            // Reinicia el contador si se mueve
            idleTimer = 0f;
        }

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
        if (isGrounded || jumpCounter < 2)
        {
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
