using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Speed of the player 
    public float speed = 0.0f;
    // Turn speed of the player
    public float turnSpeed = 0.0f;
    // Input for the player to move forward
    public float verticalInput;
    // Input for the player to turn
    public float horizontalInput;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Get the input for the player to move forward
        this.verticalInput = Input.GetAxis("Vertical");

        // Horizontal input for the player to move forward
        this.horizontalInput = Input.GetAxis("Horizontal");

        // Move the player forward
        // Time.deltaTime: The time in seconds it took to complete the last frame
        // this.speed: The speed of the player
        transform.Translate(Vector3.forward * Time.deltaTime * this.speed * this.verticalInput);

        // Turn the player
        // Time.deltaTime: The time in seconds it took to complete the last frame
        // this.turnSpeed: The turn speed of the player
        // this.horizontalInput: The input for the player to move forward
        transform.Translate(Vector3.right * Time.deltaTime * this.turnSpeed * this.horizontalInput);

        // Rotate the player
        // Vector3.up: The up direction of the player
        // Time.deltaTime: The time in seconds it took to complete the last frame
        // this.turnSpeed: The turn speed of the player
        // this.horizontalInput: The input for the player to move right
        transform.Rotate(Vector3.up, Time.deltaTime * this.turnSpeed * this.horizontalInput);
    }
}
