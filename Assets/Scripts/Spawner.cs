using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] Pokes;

    private void Start()
    {
        InvokeRepeating("IstantiatePoke", 5, 10);
    }

    void IstantiatePoke()
    {
        var poke = Pokes[Random.Range(0, Pokes.Length)];

        Instantiate(poke, transform.position, Quaternion.identity, transform);
    }
}
