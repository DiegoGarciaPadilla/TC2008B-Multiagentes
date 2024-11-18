using UnityEngine;

public class SnowflakeLaserAttack : MonoBehaviour
{
    public float rotationSpeed = 30f; // Velocidad de rotación del conjunto de láseres
    public float laserLength = 10f; // Longitud de los láseres
    public float lineWidth = 0.1f; // Ancho de los láseres
    public float spawnRadius = 1f; // Radio desde el cual se generan los láseres
    public Gradient activeLaserGradient; // Gradiente de color para los láseres activos
    public Gradient inactiveLaserGradient; // Gradiente de color para los láseres inactivos

    private LineRenderer[] lasers; // Array de láseres
    private BoxCollider2D[] colliders; // Array de colliders
    private bool isCrossActive = true; // Indica si los láseres en forma de cruz están activos
    private float toggleTimer = 0f; // Temporizador para alternar el patrón de láseres
    public float toggleInterval = 1f; // Intervalo de tiempo en segundos para alternar el patrón

    void Start()
    {
        // Inicializar los arrays de láseres y colliders
        lasers = new LineRenderer[8];
        colliders = new BoxCollider2D[8];

        for (int i = 0; i < 8; i++)
        {
            // Crear un objeto para cada láser y agregar el componente LineRenderer y BoxCollider2D
            GameObject laserObject = new GameObject("Laser" + i);
            laserObject.transform.SetParent(transform, false);

            LineRenderer lr = laserObject.AddComponent<LineRenderer>();
            lr.positionCount = 2; // Dos puntos para definir la línea
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.sortingOrder = 1;

            BoxCollider2D collider = laserObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            lasers[i] = lr;
            colliders[i] = collider;
        }

        UpdateLaserPositions();
        InitializeLaserPattern(); // Configurar los láseres activos al inicio
    }

    void Update()
    {
        // Rotar el conjunto de láseres alrededor del centro del objeto
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Alternar el patrón de láseres según el intervalo de tiempo
        toggleTimer += Time.deltaTime;
        if (toggleTimer >= toggleInterval)
        {
            toggleTimer = 0f;
            isCrossActive = !isCrossActive;
            ToggleLaserPattern();
        }

        UpdateLaserPositions();
    }

    void UpdateLaserPositions()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            float angle = i * 45f; // Ángulos para los 8 láseres (0°, 45°, 90°, ..., 315°)
            float radians = (angle + transform.eulerAngles.z) * Mathf.Deg2Rad;

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

    void InitializeLaserPattern()
    {
        // Inicializar el patrón de láseres con 4 activos y 4 inactivos
        for (int i = 0; i < lasers.Length; i++)
        {
            if (i % 2 == 0)
            {
                lasers[i].colorGradient = activeLaserGradient; // Activar los láseres en ángulos de 0°, 90°, 180°, 270°
                lasers[i].enabled = true;
                colliders[i].enabled = true;
            }
            else
            {
                lasers[i].colorGradient = inactiveLaserGradient; // Desactivar los láseres en ángulos de 45°, 135°, 225°, 315°
                lasers[i].enabled = false;
                colliders[i].enabled = false;
            }
        }
    }

    void ToggleLaserPattern()
    {
        // Alternar el color de los láseres para activar/desactivar en forma de cruz
        for (int i = 0; i < lasers.Length; i++)
        {
            if ((i % 2 == 0 && isCrossActive) || (i % 2 != 0 && !isCrossActive))
            {
                lasers[i].colorGradient = activeLaserGradient;
                lasers[i].enabled = true;
                colliders[i].enabled = true;
            }
            else
            {
                lasers[i].colorGradient = inactiveLaserGradient;
                lasers[i].enabled = false;
                colliders[i].enabled = false;
            }
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
            InitializeLaserPattern(); // Configurar el patrón inicial cuando se vuelve a activar
        }
    }
}
