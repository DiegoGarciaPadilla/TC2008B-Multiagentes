using UnityEngine;
using System.Collections.Generic;

public class GeneticPathFinder : MonoBehaviour
{

    public float creatureSpeed;
    public float pathMultiplier;
    int pathIndex = 0;
    public DNA dna;
    public bool hasFinished = false;
    bool hasBeenInitialized = false;
    Vector2 target;
    Vector2 nextPoint;

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

     public void Start()
    {
        InitCreature(new DNA(),Vector2.zero);
    }

      private void Update()
    {
        if(hasBeenInitialized && !hasFinished){
            if(pathIndex == dna.genes.Count)
            {
                hasFinished = true;
            }
            if((Vector2)transform.position == nextPoint)
            {
                nextPoint = (Vector2)transform.position + dna.genes[pathIndex];
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
