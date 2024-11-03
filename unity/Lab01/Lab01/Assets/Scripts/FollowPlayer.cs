using UnityEngine;

/// <summary>
/// FollowPlayer class for making the camera follow the player
/// </summary>
public class FollowPlayer : MonoBehaviour
{

    // Reference to the player GameObject
    public GameObject player;
    // Offset of the camera from the player
    public Vector3 offset = new Vector3(0, 6, -7);


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
        // Move the camera to follow the player
        // transform.position = player.transform.position + offset;
    }

    /// <summary>
    /// LateUpdate is called once per frame after Update
    /// </summary>
    void LateUpdate()
    {
        // Move the camera to follow the player
        transform.position = player.transform.position + offset;
    }
}
