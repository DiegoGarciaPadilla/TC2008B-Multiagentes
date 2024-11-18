using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Cambia a la escena del juego cuando se hace clic en el botón
        SceneManager.LoadScene("Level1");
    }

    public void TestMethod()
    {
        Debug.Log("El botón funciona correctamente");
    }
}
