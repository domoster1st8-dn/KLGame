using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTakeDamage : StateMachineBehaviour
{
    public ThirdMovement thirdMovement;
    private void Awake()
    {
        thirdMovement = FindObjectOfType<ThirdMovement>();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thirdMovement = animator.GetComponent<ThirdMovement>();
        thirdMovement.CanAttack = false;
        thirdMovement.CanMove = false;
        thirdMovement.CanRotation = false;
        thirdMovement.CanJump = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= .27f)
        {
            thirdMovement.CanAttack = true;
           
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thirdMovement.CanAttack = true;
        thirdMovement.CanMove = true;
        thirdMovement.CanRotation = true;
        thirdMovement.CanJump = true;
        animator.SetBool("Combo",false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
