using UnityEngine;
using TMPro;

/// <summary>
/// Class to make Chuck Norris jokes
/// </summary>
public class ChuckNorris : MonoBehaviour
{
    public TextMeshProUGUI jokeText;

    /// <summary>
    /// Get a new joke from the API and show it
    /// </summary>
    public void NewJoke(){
        Joke j = APIHelper.GetNewJoke();
        jokeText.text = j.value;
    }
}
