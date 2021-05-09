using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : StateMachineBehaviour
{
    public ThirdMovement ThirdMovement;
    public PlayerCombat playerCombat;
    public float Addforce = 7f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        ThirdMovement = animator.GetComponent<ThirdMovement>();
        playerCombat = animator.GetComponent<PlayerCombat>();
        playerCombat.UseMana();
        //ThirdMovement.AddImpact(ThirdMovement.transform.forward, Addforce);

        animator.SetBool("Combo", false);

        //ThirdMovement.CanMove = false; // Khoa di chuyen
        //ThirdMovement.CanJump = false;

        ThirdMovement.CanRotation = false;
        ThirdMovement.isAttack = true;
        
        playerCombat.OnCombo();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ThirdMovement.CanMove = false; // Khoa di chuyen
        ThirdMovement.CanJump = false;
        ThirdMovement.controller.SimpleMove(ThirdMovement.transform.forward * 0.5f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ThirdMovement.CanMove = true; // Mo Khoa di chuyen
        ThirdMovement.CanJump = true;
     
        ThirdMovement.isAttack = false;

        
        if (playerCombat.AttackNum <= 2)
        {
            playerCombat.ResetCombo();
            
            return;
        }
        else
        {
            if (Quaternion.Angle(ThirdMovement.transform.rotation, ThirdMovement.angle) != 0)
            {
                ThirdMovement.transform.rotation = ThirdMovement.angle;
            }
        }

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
