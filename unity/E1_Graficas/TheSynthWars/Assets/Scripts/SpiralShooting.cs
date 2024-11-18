using UnityEngine;

public class SpiralShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public BulletCounter bulletCounter; // Referencia al contador de balas
    public float bulletSpeed = 2f; // Velocidad de las balas
    public float shootInterval = 0.2f; // Tiempo entre cada conjunto de disparos
    public float spawnRadius = 1f; // Radio desde el cual se dispararán las balas
    private float shootTimer = 0f;
    private float angle = 0f; // Ángulo inicial

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            ShootSpiralBullets();
            shootTimer = 0f;
        }
    }

    void ShootSpiralBullets()
    {
        // Dispara dos balas en direcciones opuestas
        ShootBulletAtAngle(angle); // Bala en una dirección
        ShootBulletAtAngle(angle + 90f); // Bala en una dirección perpendicular
        ShootBulletAtAngle(angle + 180f); // Bala en la dirección opuesta
        ShootBulletAtAngle(angle + 270f); // Bala en la dirección perpendicular opuesta

        // Incrementa el ángulo para la próxima iteración
        angle += 10f; // Ajusta este valor para cambiar la densidad de la espiral
        if (angle >= 360f)
        {
            angle -= 360f;
        }
    }

    void ShootBulletAtAngle(float currentAngle)
    {
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calcula la posición de inicio de la bala usando el radio
        Vector2 spawnPosition = new Vector2(
            transform.position.x + spawnRadius * Mathf.Cos(radians),
            transform.position.y + spawnRadius * Mathf.Sin(radians)
        );

        // Instancia la bala en la posición calculada
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Asigna la velocidad de la bala en la dirección calculada
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        rb.linearVelocity = direction * bulletSpeed;

        // Ajusta la rotación de la bala para que apunte en la dirección de su movimiento
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

        // Asigna el BulletCounter a la bala y aumenta el contador
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null && bulletCounter != null)
        {
            bulletScript.SetBulletCounter(bulletCounter);
            bulletCounter.IncrementBulletCount(); // Incrementa el contador de balas
        }
    }
}
