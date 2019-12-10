using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] Pokes;
    [SerializeField]
    GameObject[] shinies;
    [SerializeField]
    int ShinyChance = 4096;
    int ShinyChanceModify = 4096;
    string lastKilled = "Start";

    /// <summary>
    /// esto es para ir mejorando la posibilidad de shinies conforme se juega.
    /// La idea es que la probabilidad de shiny comienza fixeada en 1/4096 pero
    /// si se matan varios de la misma especie en racha el numero irá mejorando hasta que eventalmente 
    /// cada pokemon instanciado sea shiny
    /// </summary>
    public void NotifyKill(string PokemonName)
    {
        //si el que se acaba de matar no es el mismo que el último matado entonces se reinicia el número
        if (lastKilled != PokemonName) 
        {
            ShinyChanceModify = ShinyChance;
        }
        else //si sí es el mismo,se reduce ligeramente
        {
            ShinyChanceModify -= 10;
        }

        if (ShinyChanceModify <= 1) //esta es la probabilidad mínima
            ShinyChanceModify = 1;

        lastKilled = PokemonName; //actualizamos último
    }

    private void Start()
    {
        if (StaticManager.spawner == null)
            StaticManager.spawner = this;

        ShinyChanceModify = ShinyChance;
        InvokeRepeating("IstantiatePoke", 5, 10);
    }

    void IstantiatePoke()
    {
        //verificar si el jugador está mirando al punto de spawn
        Vector3 plyrfwrd = StaticManager.player.transform.forward.normalized;
        Vector3 fwrd = (transform.position - StaticManager.player.transform.position).normalized;

        float dotProduct = Vector3.Dot(plyrfwrd, fwrd);
        if (dotProduct > 0) return;

        int indexPoke = Random.Range(0, Pokes.Length); //seleciconamos pokemon
        
        //posibilidad de 1/4096 de que sea shiny.
        var poke = Random.Range(0, ShinyChanceModify) == 0 ? shinies[indexPoke] : Pokes[indexPoke];

        Instantiate(poke, transform.position, Quaternion.identity, transform);
    }
}
