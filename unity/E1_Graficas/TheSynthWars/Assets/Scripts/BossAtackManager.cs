using UnityEngine;

public class BossAttackManager : MonoBehaviour
{

    // Combinaciones de ataques, donde cada número representa un modo de ataque
    // 0: SpiralShooting
    // 1: WaveShooting
    // 2: RotatingLaserCross
    // 3: SnowflakeLaserAttack
    private int[][] attackCombinations = new int[][]
    {
        // Intro
        new int[] { 0 },
        new int[] { 1 },
        // Ataque 1
        new int[] { 2 },
        new int[] { 1, 2 },
        new int[] { 0, 1 },
        new int[] { 0, 2 },
        // Ataque 2
        new int[] { 1 },
        new int[] { 0, 1 },
        // Ataque 3
        new int[] { 3 },
        // Ataque 4
        new int[] { 0, 3 },
    };

    public float bpm = 115f; // Beats por minuto de la música
    public int barsBetweenModes = 4; // Número de compases entre cada cambio de modo
    public MonoBehaviour[] attackModes; // Array de scripts de los distintos modos de ataque
    public AudioSource audioSource; // Referencia al AudioSource que reproduce la música

    private int currentCombinationIndex = 0; // Índice en el array de la combinación de ataques actual
    private float timeBetweenModes; // Tiempo en segundos entre cada cambio de combinación
    private float audioStartTime; // Momento en el que se inicia el audio

    void Start()
    {
        // Calcular el tiempo entre modos de ataque basado en BPM y compases
        float secondsPerBeat = 60f / bpm;
        timeBetweenModes = secondsPerBeat * 4 * barsBetweenModes; // 4 beats por compás

        // Iniciar el audio y registrar el tiempo de inicio
        if (audioSource != null)
        {
            audioSource.Play();
            audioStartTime = Time.time; // Registrar el tiempo en que empieza el audio
        }

        // Inicializar la primera combinación de ataques
        ActivateAttackCombination(currentCombinationIndex);
    }

    void Update()
    {
        // Sincronizar el temporizador con el tiempo de reproducción del audio
        float elapsedAudioTime = Time.time - audioStartTime;

        // Verificar si es el momento de cambiar a la siguiente combinación de ataques
        if (elapsedAudioTime >= timeBetweenModes * (currentCombinationIndex + 1))
        {
            currentCombinationIndex++;
            if (currentCombinationIndex >= attackCombinations.Length)
            {
                currentCombinationIndex = 0; // Volver al inicio de la secuencia
                audioStartTime = Time.time; // Reiniciar el tiempo de inicio para sincronizar
            }
            ActivateAttackCombination(currentCombinationIndex); // Activar la nueva combinación de ataques
        }
    }

    void ActivateAttackCombination(int combinationIndex)
    {
        // Desactivar todos los modos de ataque
        for (int i = 0; i < attackModes.Length; i++)
        {
            attackModes[i].enabled = false;
        }

        // Activar la combinación de ataques seleccionada
        if (combinationIndex >= 0 && combinationIndex < attackCombinations.Length)
        {
            foreach (int index in attackCombinations[combinationIndex])
            {
                if (index >= 0 && index < attackModes.Length)
                {
                    attackModes[index].enabled = true;
                    Debug.Log("Modo de ataque activado: " + attackModes[index].GetType().Name);
                }
            }
        }
    }
}
