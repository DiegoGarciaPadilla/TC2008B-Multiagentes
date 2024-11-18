using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletCounter bulletCounter;

    // Método para inicializar el contador de balas
    public void SetBulletCounter(BulletCounter counter)
    {
        bulletCounter = counter;
    }

    void Start()
    {
        // Rota la bala hacia la dirección en la que se mueve
        RotateTowardsDirection();
    }

    void OnDestroy()
    {
        if (bulletCounter != null)
        {
            bulletCounter.DecrementBulletCount(); // Decrementa el contador de balas
        }
    }

    private void RotateTowardsDirection()
    {
        // Obtén el vector de velocidad de la bala
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 velocity = rb.linearVelocity;
            if (velocity != Vector2.zero)
            {
                // Calcula el ángulo en radianes y lo convierte a grados
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;

                // Aplica la rotación a la bala
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
}
