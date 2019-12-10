using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(NavMeshAgent))]
public class Pokemon : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    Animator anim;
    public Transform SpawnPoint;
    public GameObject ProyectilePrefab;
    public string PokemonName;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (target == null)
        {
            target = FindObjectOfType<FirstPersonController>().transform;
        }

        agent.speed = 1.5f;
    }

    public NavMeshAgent GetAgentInstance()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        return agent;
    }

    public void Shoot()
    {
        Instantiate(ProyectilePrefab, SpawnPoint.position, SpawnPoint.rotation);
    }

    public void IsTargetClose()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        anim.SetFloat("EnemyDistance", distance);
    }

    public void GoToTarget()
    {
        agent.SetDestination(target.position);
    }
}
