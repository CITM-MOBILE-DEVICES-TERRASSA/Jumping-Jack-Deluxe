using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private ParticleSystem derecha;
    [SerializeField] private ParticleSystem izquierda;
    public AudioSource slideSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f) 
        {
            if (!slideSound.isPlaying)
            {
                slideSound.Play(); 
            }

            if (rb.velocity.x > 0)
            {
                if (!derecha.isPlaying)
                    derecha.Play();
                if (izquierda.isPlaying)
                    izquierda.Stop();
            }
            else if (rb.velocity.x < 0)
            {
                if (!izquierda.isPlaying)
                    izquierda.Play();
                if (derecha.isPlaying)
                    derecha.Stop();
            }
        }
        else
        {
            if (slideSound.isPlaying)
            {
                slideSound.Stop();
            }

            if (derecha.isPlaying)
                derecha.Stop();
            if (izquierda.isPlaying)
                izquierda.Stop();
        }
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
