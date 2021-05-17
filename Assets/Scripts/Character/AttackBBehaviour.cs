using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBBehaviour : StateMachineBehaviour
{
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = stateInfo.length;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;

        if (timer <= stateInfo.length * 50 / 100)
            Character_Combat.combInstance.canAttack = true;
        
        if (timer <= 0)
            Character_Combat.combInstance.isAttacking = false;


        if (Character_Combat.combInstance.canAttack)
            if (Input.GetButtonDown("Attack"))
            {
                Character_Animation_Manager.animInstance.ChangeAnimationState("Attack C");
                Character_Combat.combInstance.canAttack = false;
                Character_Combat.combInstance.isAttacking = true;
            }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
