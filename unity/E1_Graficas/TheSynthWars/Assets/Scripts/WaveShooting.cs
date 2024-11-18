using UnityEngine;

public class WaveShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public BulletCounter bulletCounter; // Referencia al contador de balas
    public float bulletSpeed = 2f; // Velocidad de las balas
    public float shootInterval = 0.2f ; // Intervalo de tiempo en segundos entre cada disparo
    public int bulletsPerWave = 20; // Número de balas en cada disparo de onda
    public float spreadAngle = 360f; // Ángulo de dispersión de las balas en la onda
    public float spawnRadius = 1f; // Radio desde el cual se dispararán las balas

    private float shootTimer = 0f;
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            ShootWaveBullets();
            shootTimer = 0f;
        }
    }

    void ShootWaveBullets()
    {
        float angleStep = spreadAngle / (bulletsPerWave - 1); // Espaciado entre las balas
        float startAngle = -spreadAngle / 2; // Ángulo inicial

        for (int i = 0; i < bulletsPerWave; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
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
            Vector2 velocity = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * bulletSpeed;
            rb.linearVelocity = velocity;

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
}
