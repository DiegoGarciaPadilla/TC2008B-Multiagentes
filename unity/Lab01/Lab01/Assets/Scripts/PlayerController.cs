using UnityEngine;

/// <summary>
/// PlayerController class for controlling the player vehicle
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Speed of the vehicle
    public float speed = 10.0f;
    // Maximum turn speed
    public float turnSpeed = 100.0f;
    // Decay factor for turn speed when moving slower
    public float turnDampening = 0.5f;
    // Input values for movement and turning
    private float verticalInput;
    private float horizontalInput;

    // Multiplayer support
    public string inputId;

    // Cameras for the player vehicle
    public Camera mainCamera;
    // Camera for the hood view
    public Camera hoodCamera;
    // Key to switch between cameras
    public KeyCode switchKey = KeyCode.C;

    // Rigidbody for realistic physics-based movement
    private Rigidbody rb;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Get input values for movement and turning
        verticalInput = Input.GetAxis("Vertical" + inputId);
        horizontalInput = Input.GetAxis("Horizontal" + inputId);

        // Forward movement based on vertical input and speed
        Vector3 moveDirection = transform.forward * verticalInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Turn only if the vehicle is moving (i.e., verticalInput is non-zero)
        if (Mathf.Abs(verticalInput) > 0.1f)
        {
            // Calculate turn speed based on current speed, dampened by turnDampening factor
            float adjustedTurnSpeed = turnSpeed * turnDampening * Mathf.Abs(verticalInput);
            float turn = horizontalInput * adjustedTurnSpeed * Time.deltaTime;

            // Apply rotation around the Y axis (up)
            Quaternion turnOffset = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnOffset);
        }

        // Switch between cameras when the switch key is pressed
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
