using UnityEngine;

public class BossAttackManager : MonoBehaviour
{
    public float bpm = 120f; // Beats por minuto de la música
    public int barsBetweenModes = 4; // Número de compases entre cada cambio de modo
    public MonoBehaviour[] attackModes; // Array de scripts de los distintos modos de ataque (RotatingLaserCross, WaveShooting, SpiralShooting)
    private int currentAttackIndex = 0; // Índice del modo de ataque actual
    private float timeBetweenModes; // Tiempo en segundos entre cada cambio de modo
    private float timer = 0f; // Temporizador para controlar los cambios de modo

    void Start()
    {
        // Calcular el tiempo entre modos de ataque basado en BPM y compases
        float secondsPerBeat = 60f / bpm;
        timeBetweenModes = secondsPerBeat * 4 * barsBetweenModes; // 4 beats por compás

        // Inicializar el primer modo de ataque
        ActivateAttackMode(currentAttackIndex);
    }

    void Update()
    {
        // Actualizar el temporizador
        timer += Time.deltaTime;

        // Si el temporizador ha superado el tiempo entre modos, cambiar de modo de ataque
        if (timer >= timeBetweenModes)
        {
            timer = 0f; // Reiniciar el temporizador
            currentAttackIndex = (currentAttackIndex + 1) % attackModes.Length; // Avanzar al siguiente modo de ataque
            ActivateAttackMode(currentAttackIndex); // Activar el nuevo modo de ataque
        }
    }

    void ActivateAttackMode(int index)
    {
        // Desactivar todos los modos de ataque
        for (int i = 0; i < attackModes.Length; i++)
        {
            attackModes[i].enabled = false;
        }

        // Activar el modo de ataque seleccionado
        if (attackModes.Length > 0)
        {
            attackModes[index].enabled = true;
            Debug.Log("Modo de ataque activado: " + attackModes[index].GetType().Name);
        }
    }
}
