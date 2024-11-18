using UnityEngine;

public class RotatingLaserCross : MonoBehaviour
{
    public float rotationSpeed = 30f; // Velocidad de rotación de la cruz
    public float laserLength = 10f; // Longitud de los láseres
    public float lineWidth = 0.1f; // Ancho de los láseres
    public float spawnRadius = 1f; // Radio desde el cual se generarán los láseres
    public Gradient laserGradient; // Gradiente de color para el efecto de láser

    private LineRenderer[] lasers; // Array de láseres
    private BoxCollider2D[] colliders; // Array de colliders

    void Start()
    {
        // Inicializar los arrays de láseres y colliders
        lasers = new LineRenderer[4];
        colliders = new BoxCollider2D[4];

        for (int i = 0; i < 4; i++)
        {
            // Crear un objeto para cada láser y agregar el componente LineRenderer y BoxCollider2D
            GameObject laserObject = new GameObject("Laser" + i);
            laserObject.transform.SetParent(transform, false); // Mantener la transformación relativa al padre

            LineRenderer lr = laserObject.AddComponent<LineRenderer>();
            lr.positionCount = 2; // Dos puntos para definir la línea
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.material = new Material(Shader.Find("Sprites/Default")); // Material simple para colores y transparencia
            lr.colorGradient = laserGradient; // Aplica el gradiente de color al láser
            lr.sortingOrder = 1; // Asegura que el láser esté al frente

            BoxCollider2D collider = laserObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true; // Configurar como Trigger

            lasers[i] = lr;
            colliders[i] = collider;
        }

        UpdateLaserPositions(); // Configurar las posiciones iniciales de los láseres
    }

    void Update()
    {
        // Rotar la cruz de láseres alrededor del centro del objeto en la misma dirección que el objeto padre
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        UpdateLaserPositions(); // Actualizar las posiciones y rotaciones de los láseres
    }

    void UpdateLaserPositions()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            float angle = i * 90f; // 0°, 90°, 180°, y 270° para formar una cruz
            float radians = (angle + transform.eulerAngles.z) * Mathf.Deg2Rad; // Sumar la rotación del objeto padre

            // Calcular la posición de inicio y fin del láser en relación al objeto padre
            Vector3 startPoint = transform.position + new Vector3(
                spawnRadius * Mathf.Cos(radians),
                spawnRadius * Mathf.Sin(radians),
                0
            );

            Vector3 endPoint = startPoint + new Vector3(
                laserLength * Mathf.Cos(radians),
                laserLength * Mathf.Sin(radians),
                0
            );

            lasers[i].SetPosition(0, startPoint);
            lasers[i].SetPosition(1, endPoint);

            // Actualizar el BoxCollider2D para que coincida con el láser
            Vector2 colliderCenter = (startPoint + endPoint) / 2;
            colliders[i].offset = colliders[i].transform.InverseTransformPoint(colliderCenter);
            colliders[i].size = new Vector2(laserLength * 2, lineWidth);
            colliders[i].transform.rotation = Quaternion.Euler(0, 0, angle + transform.eulerAngles.z);
        }
    }

        void OnDisable()
    {
        // Desactivar todos los láseres y colliders cuando el script se desactiva
        if (lasers != null)
        {
            foreach (LineRenderer lr in lasers)
            {
                lr.enabled = false;
            }

            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    void OnEnable()
    {
        // Reactivar los láseres y los colliders cuando el script se activa
        if (lasers != null)
        {
            foreach (LineRenderer lr in lasers)
            {
                lr.enabled = true;
            }

            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
    }


}
