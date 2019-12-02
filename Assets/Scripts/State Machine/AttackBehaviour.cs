using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    Pokemon AssociatedP;
    Transform InstancePoint;
    GameObject ProyectilePrefab;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssociatedP = animator.gameObject.GetComponent<Pokemon>();
        InstancePoint = AssociatedP.SpawnPoint;
        ProyectilePrefab = AssociatedP.ProyectilePrefab;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }
}
