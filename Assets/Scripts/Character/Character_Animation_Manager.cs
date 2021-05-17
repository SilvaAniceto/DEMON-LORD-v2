using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animation_Manager : MonoBehaviour
{
    public static Character_Animation_Manager animInstance;

    Rigidbody2D rb;
    Animator animator;
    private string currentState;

    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string JUMP = "Jump";
    const string FALL = "Fall";
    const string WALLSLIDE = "Wallslide";

    void Awake()
    {
        animInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character_Combat.combInstance.isAttacking)
        {
            if (Character_Moviment.moveInstance.grounded)
            {
                if (Character_Moviment.moveInstance.moveInput != 0 && !Character_Moviment.moveInstance.wallSliding)
                    ChangeAnimationState(WALK);
                else
                    ChangeAnimationState(IDLE);
            }
            else
            {
                if (Character_Moviment.moveInstance.wallSliding)
                    ChangeAnimationState(WALLSLIDE);
                else if (rb.velocity.y > 0.2f)
                    ChangeAnimationState(JUMP);
                else if (rb.velocity.y < 0.2f)
                    ChangeAnimationState(FALL);
                else
                    return;
            }
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);

        currentState = newState;
    }
}
