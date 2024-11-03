using UnityEngine;
using System.Net;
using System.IO;

/// <summary>
/// Class to help us to call the API
/// </summary>
public static class APIHelper
{
    /// <summary>
    /// Get a new joke from the API
    /// </summary>
    /// <returns>
    /// A Joke object with the joke information
    /// </returns>
    public static Joke GetNewJoke()
    {
        // Create the request
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://api.chucknorris.io/jokes/random");

        // Get the response
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        // Read the response
        StreamReader reader = new StreamReader(response.GetResponseStream());

        // Get the JSON as a string
        string json = reader.ReadToEnd();

        // Parse the JSON to a Joke object and return it
        return JsonUtility.FromJson<Joke>(json);
    }
}