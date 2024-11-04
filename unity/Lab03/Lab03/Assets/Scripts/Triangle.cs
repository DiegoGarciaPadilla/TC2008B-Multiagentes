using UnityEngine;
using System;
using System.Collections;

public class Triangle : MonoBehaviour
{
    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    /// <summary>
    /// Moves the square.
    /// </summary>
    private void TimeCheck()
    {
        // NOTE: I reduced the time to 0:10 for testing purposes.
        // Every time the hour is 0 and the minute is a multiple of 10, move the square.
        if(TimeManager.Hour == 0 && TimeManager.Minute % 5 == 0)
        {
            StartCoroutine(MoveTriangle());
        }
    } 

    /// <summary>
    /// Moves the triangle from the left to the right.
    /// </summary>
    private IEnumerator MoveTriangle()
    {
        transform.position = new Vector3(278, 261, 0);
        Vector3 targetPos = new Vector3(252, 261, 0);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 1;

        while (timeElapsed < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed / timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set to the target position
        transform.position = targetPos;
    }
}
