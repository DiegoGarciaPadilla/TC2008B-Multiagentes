using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Calcula los límites de la cámara en unidades del mundo
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }
    }

    void Update()
    {
        // Verifica si el objeto está fuera de los límites de la cámara
        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x ||
            transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
