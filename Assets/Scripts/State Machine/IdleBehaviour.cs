using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBehaviour : StateMachineBehaviour
{
    Pokemon AssociatedP;
    NavMeshAgent agent;
    Vector3 PointA = new Vector3(-100, -100), PointB = new Vector3(-100, -100); //Moving from A to B until it sees player

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AssociatedP = animator.gameObject.GetComponent<Pokemon>();
        agent = AssociatedP.GetAgentInstance();

        PointA = AssignatePoint(AssociatedP.transform.position);
        PointB = AssignatePoint(PointA);

        agent.SetDestination(PointA); // Go to A
    }

    

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //Move from A to B and vice versa
        if (Vector3.Distance(AssociatedP.transform.position, PointA) < 0.5f)
            agent.SetDestination(PointB);
        else if (Vector3.Distance(AssociatedP.transform.position, PointB) < 0.5f)
            agent.SetDestination(PointA);

        //check if target is close
        AssociatedP.IsTargetClose();
    }

    Vector3 AssignatePoint(Vector3 initialP)
    {
        Vector3 position = initialP;
        Vector3 p = new Vector3(-100, -100);

        while (p == new Vector3(-100, -100))
        {
            var possiblePos = position + (Random.insideUnitSphere * 5);
            p = new Vector3(possiblePos.x, position.y, possiblePos.z);

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(p, path);

            if (path.status != NavMeshPathStatus.PathComplete)
                p = new Vector3(-100, -100);
            else if (Vector3.Distance(initialP, p) < 3.5f)
                p = new Vector3(-100, -100);
        }

        return p;
    }
}
