using UnityEngine;
using System.Collections.Generic;

public class PopulationController : MonoBehaviour
{
    /// <summary>
    /// Lista de criaturas
    /// </summary>
    List<GeneticPathFinder> population = new List<GeneticPathFinder>();

    /// <summary>
    /// Prefab de la criatura
    /// </summary>
    public GameObject creaturePrefab;

    /// <summary>
    /// Configuración de la población
    /// </summary>
    public int populationSize = 100;

    /// <summary>
    /// Longitud del genoma
    /// </summary>
    public int genomeLength;

    /// <summary>
    /// Porcentaje de la población que se sobrevive
    /// </summary>
    public float cutoff = 0.3f; //how much will die or survive

    /// <summary>
    /// Spawn point de las criaturas
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// Punto final del camino
    /// </summary>
    public Transform end;


    // Start is called before the first frame update
    private void Start()
    {
        // Inicializar la población
        InitPopulation();
    }

    // Update is called once per frame
    private void Update()
    {
        // ¿Hay individuos activos?
        if(!HasActive())
        {
            // Seleccionar los individuos que sobreviven
            NextGeneration();
        }
    }

    /// <summary>
    /// Inicialización de la población
    /// </summary>
    void InitPopulation()
    {
        // Crear la población
        for(int i =0; i < populationSize; i++)
        {
            // Instanciar la criatura
            GameObject go = Instantiate(creaturePrefab,spawnPoint.position,Quaternion.identity);
            // Inicializar la criatura
            go.GetComponent<GeneticPathFinder>().InitCreature(new DNA(genomeLength),end.position);
            // Añadir la criatura a la población
            population.Add(go.GetComponent<GeneticPathFinder>());
        }
    }

    /// <summary>
    /// Método de selección de los individuos que sobreviven
    /// </summary>
    /// <returns>bool que indica si hay individuos activos</returns>
    bool HasActive()
    {
        for(int i = 0; i <population.Count;i++)
        {
            // Si hay individuos activos, devolver true
            if(!population[i].hasFinished)
            {
                return true;
            }
        }

        // Si no hay individuos activos, devolver false
        return false;
    }

    /// <summary>
    /// Método de selección de los individuos que sobreviven
    /// </summary>
    /// <returns>Criatura más apta</returns>
    GeneticPathFinder GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;
        for(int i = 0; i < population.Count; i++)
        {
            if(population[i].fitness > maxFitness){
                maxFitness = population[i].fitness;
                index = i;
            }
        }
        GeneticPathFinder fittest = population[index];
        population.Remove(fittest);
        return fittest;
    }

    /// <summary>
    /// Método de selección de los individuos que sobreviven
    /// </summary>
    void NextGeneration()
    {
        // ¿Cuántos individuos sobreviven?
        int survivorCut = Mathf.RoundToInt(populationSize*cutoff);

        // Lista de supervivientes
        List<GeneticPathFinder> survivors = new List<GeneticPathFinder>();

        // Añadir los supervivientes
        for(int i = 0; i < survivorCut;i++)
        {
            survivors.Add(GetFittest());
        }

        // Destruir la generación anterior
        for(int i = 0; i < population.Count;i++)
        {
            Destroy(population[i].gameObject);
        }

        // Limpiar la lista de la población (elementos en pantalla)
        population.Clear();

        // Crear la nueva generación
        while(population.Count < populationSize)
        {
            // Crear una nueva criatura
            for(int i = 0; i < survivors.Count;i++)
            {
                // Instanciar la criatura
                GameObject go = Instantiate(creaturePrefab,spawnPoint.position,Quaternion.identity);

                // Inicializar la criatura
                go.GetComponent<GeneticPathFinder>().InitCreature(new DNA(survivors[i].dna,survivors[Random.Range(0,10)].dna),end.position);

                // Añadir la criatura a la población
                population.Add(go.GetComponent<GeneticPathFinder>());

                // ¿Hemos llegado al tamaño de la población?
                if(population.Count >= populationSize)
                {
                    // Salir del bucle
                    break;
                }
            }
        }

        // Destruir los supervivientes
        for(int i = 0; i < survivors.Count;i++)
        {
            Destroy(survivors[i].gameObject);
        }

    }

    
}
