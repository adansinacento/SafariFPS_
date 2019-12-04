using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : StateMachineBehaviour
{
    Pokemon AssociatedP;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AssociatedP = animator.gameObject.GetComponent<Pokemon>();
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AssociatedP.GoToTarget();
        AssociatedP.IsTargetClose();
    }
}
