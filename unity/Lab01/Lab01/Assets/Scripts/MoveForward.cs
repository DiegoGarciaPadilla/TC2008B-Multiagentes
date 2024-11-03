using UnityEngine;

/// <summary>
/// MoveForward class for moving an object forward
/// </summary>
public class MoveForward : MonoBehaviour
{
    // Speed of the object
    public int speed = 10;    

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
        // Move the object forward
        transform.Translate( Vector3.forward * this.speed * Time.deltaTime);
    }
}
