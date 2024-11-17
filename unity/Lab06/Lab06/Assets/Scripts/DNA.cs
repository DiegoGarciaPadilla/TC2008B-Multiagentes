using UnityEngine;

/// <summary>
/// Clase que contiene los componentes que integran una criatura
/// </summary>
public class DNA
{

    public List<Vector2> genes = new List<Vector2>();

    /// <summary>
    /// Constructor de la clase DNA que inicializa los genes de la criatura
    /// </summary>
    /// <param name="genomeLength">Longitud del genoma</param>
    public DNA(int genomeLength = 50)
    {
        for(int i = 0; i < genomeLength; i++)
        {
            genes.Add(new Vector2(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f)));
        }
    }

    /// <summary>
    /// Constructor de la clase DNA que inicializa los genes de la criatura a partir de los genes de sus padres
    /// </summary>
    /// <param name="parent">Padre de la criatura</param>
    /// <param name="partner">Madre de la criatura</param>
    /// <param name="mutationRate">Tasa de mutación de la criatura</param>
    public DNA(DNA parent,DNA partner,float mutationRate=0.01f)
    {
        for(int i = 0; i < parent.genes.Count; i++)
        {
            float mutationChance = Random.Range(0.0f,1.0f);
            if(mutationChance < mutationRate)
            {
                genes.Add(new Vector2(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f)));
            }
            else
            {
                int chance = Random.Range(0,2);
                if(chance == 0)
                {
                    genes.Add(parent.genes[i]);
                }
                else
                {
                    genes.Add(partner.genes[i]);
                }
            }
        }
    }


}
