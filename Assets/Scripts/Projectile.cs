using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 5f;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
