using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private bool canDoubleJump = true;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Slide")]
    [SerializeField] private float slideSpeed = 15f;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float slideCooldown = 1f;
    [SerializeField] private float slideYOffset = 0.5f;

    [Header("Dead")]
    public Color screenDieTint = Color.red;
    public float stopingTime = 0.5f;
    private float stopingCurrentTime = 0f;
    private float lastXPosition;
    [SerializeField] private Volume globalVolume;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    private bool isFacingRight = true;
    private bool hasDoubleJumped;
    private bool isSliding;
    private bool canSlide = true;
    private float slideTimeLeft;
    private float currentSlideSpeed;

    private bool isDead = false;

    public bool IsFacingRight => isFacingRight;
    public bool IsInAir => !IsGrounded;
    private bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        
        HandleMovement();
        HandleSlide();
        HandleStopingDead();

        GetComponent<Animator>().SetBool("IsGrounded", IsGrounded);
    }

    private void HandleMovement()
    {
        if (isSliding) return;
        if (isDead) return;

        if (IsGrounded) hasDoubleJumped = false;
        rb.velocity = new Vector2(moveSpeed * GetDirection(), rb.velocity.y);
    }

    private void HandleSlide()
    {
        if (!isSliding) return;
        if (isDead) return;

        slideTimeLeft -= Time.deltaTime;
        float slideProgress = 1 - (slideTimeLeft / slideDuration);
        currentSlideSpeed = Mathf.Lerp(slideSpeed, moveSpeed, slideProgress);
        rb.velocity = new Vector2(currentSlideSpeed * GetDirection(), rb.velocity.y);

        if (slideTimeLeft <= 0) EndSlide();
    }


    private void HandleStopingDead()
    {
        float difference = lastXPosition - transform.position.x;

        if (Mathf.Abs(difference) > 0.0001)
        {
            lastXPosition = transform.position.x;
            stopingCurrentTime = 0f;

            if (globalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, 0, Time.deltaTime * 4);
                colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, Color.white, Time.deltaTime * 4);
            }

        }
        else
        {
            if (globalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
            {
                colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, -10, Time.deltaTime);
                colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, screenDieTint, Time.deltaTime);
            }
            stopingCurrentTime += Time.deltaTime;

            if(stopingCurrentTime > stopingTime)
            {
                Die();
            }
        }

       

    }


    public void OnJumpButtonPressed()
    {
        if (IsGrounded)
        {
            Jump();
        }
        else if (canDoubleJump && !hasDoubleJumped)
        {
            DoubleJump();
        }
    }

    private void Jump()
    {
        if (isDead) return;
        GetComponent<Animator>().SetTrigger("Jump");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void DoubleJump()
    {
        if (isDead) return;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        Jump();
        hasDoubleJumped = true;
    }

    public void OnSlideButtonPressed()
    {
        if (canSlide && !isSliding && IsGrounded) StartSlide();
    }

    private void StartSlide()
    {
        isSliding = true;
        canSlide = false;
        slideTimeLeft = slideDuration;
        currentSlideSpeed = slideSpeed;
        
        rb.velocity = new Vector2(slideSpeed * GetDirection(), rb.velocity.y);
        UpdatePosition(slideYOffset, true);
        StartCoroutine(SlideCooldown());
    }

    private void EndSlide()
    {
        isSliding = false;
        UpdatePosition(slideYOffset, false);
    }

    private void UpdatePosition(float offset, bool isStarting)
    {
        Vector3 newPosition = transform.position;
        newPosition.y += isStarting ? -offset : offset;
        transform.position = newPosition;
        transform.localScale = new Vector3(transform.localScale.x, isStarting ? 1f : 2f, transform.localScale.z);
    }

    public void OnFlipButtonPressed() => Flip();

    private float GetDirection()
    {
        if(!isDead) return isFacingRight ? 1f : -1f;
        return 0f;
    }

    private void Flip()
    {
        if (isDead) return;
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        FlipChildSprites();
    }

    //FUNCIÓN PARA FLIPEAR LA GORRA - BORRAR SI EL SPRITE ORIGINAL NO TIENE HIJOS
    private void FlipChildSprites()
    {
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            if (sprite != spriteRenderer) sprite.flipX = !isFacingRight;
        }
    }

    private IEnumerator SlideCooldown()
    {
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
    }

    private void Die()
    {
        if (isDead) return;
        GetComponent<Animator>().Play("player-Die");
        isDead = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trampa"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trampa"))
        {
            Die();
        }
    }
}