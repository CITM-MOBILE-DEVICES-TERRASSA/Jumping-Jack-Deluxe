using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detecta la fuerza que aplica el jugador al objeto
            Vector2 pushDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            rb.AddForce(pushDirection * 1f); // Ajusta la fuerza de empuje
        }
    }
}
