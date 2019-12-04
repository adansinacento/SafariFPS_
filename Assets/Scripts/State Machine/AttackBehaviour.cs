using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    Pokemon AssociatedP;
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssociatedP = animator.gameObject.GetComponent<Pokemon>();
        AssociatedP.InvokeRepeating("Shoot", 0.5f, 2f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        AssociatedP.IsTargetClose();
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AssociatedP.CancelInvoke("Shoot");
    }

    
}
