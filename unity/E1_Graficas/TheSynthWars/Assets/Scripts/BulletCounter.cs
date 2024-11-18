using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCountText;
    private int bulletCount = 0;

    // Método para actualizar el contador
    public void IncrementBulletCount()
    {
        bulletCount++;
        UpdateUI();
    }

    // Método para actualizar el texto en pantalla
    public void DecrementBulletCount()
    {
        bulletCount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        bulletCountText.text = "Bullets = " + bulletCount.ToString();
    }
}
