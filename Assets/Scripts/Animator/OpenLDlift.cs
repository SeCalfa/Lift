using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLDlift : StateMachineBehaviour
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<DoorLinker>().GetGameManager.GetLift.liftState = Lift.LiftState.DoorOpening;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
