using UnityEngine;
using System.Collections.Generic;

public class GeneticPathFinder : MonoBehaviour
{

    /// <summary>
    /// Velocidad de la criatura
    /// </summary>
    public float creatureSpeed;

    /// <summary>
    /// Ayuda a mejorar la optimización de genes
    /// </summary>
    public float pathMultiplier;

    /// <summary>
    /// Indice del gen visto por la criatura
    /// </summary>
    int pathIndex = 0;

    /// <summary>
    /// Genes de la criatura
    /// </summary>
    public DNA dna;

    /// <summary>
    /// Indica si la criatura ha terminado su recorrido
    /// </summary>
    public bool hasFinished = false;

    /// <summary>
    /// Indica si la criatura ha sido inicializada
    /// </summary>
    bool hasBeenInitialized = false;

    /// <summary>
    /// Objetivo de la criatura
    /// </summary>
    Vector2 target;

    /// <summary>
    /// Punto siguiente de la criatura
    /// </summary>
    Vector2 nextPoint;

    /// <summary>
    /// Evaua si la criatura es apta para pasar sus genes a la siguiente generación
    /// </summary>
    public float fitness
    {
        get
        {
            float dist = Vector2.Distance(transform.position,target);
            if(dist == 0)
            {
                dist = 0.0001f;
            }
            return 60/dist;
        }
    }


    private void Update()
    {
        if(hasBeenInitialized && !hasFinished){
            if(pathIndex == dna.genes.Count || Vector2.Distance(transform.position,target)<0.5f)
            {
                hasFinished = true;
            }
            if((Vector2)transform.position == nextPoint)
            {
                nextPoint = (Vector2)transform.position + dna.genes[pathIndex] * pathMultiplier;
                pathIndex++;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position,nextPoint,creatureSpeed*Time.deltaTime);
            }
        }
    }

     public void InitCreature(DNA newDna, Vector2 _target)
     {
        dna = newDna;
        target = _target;
        nextPoint = transform.position;
        hasBeenInitialized = true;
    }

}
