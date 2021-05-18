using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Combat : MonoBehaviour
{
    public static Character_Combat combInstance;

    public bool canAttack;
    public bool isAttacking;
    public CircleCollider2D attackHitBox;

    public bool isBlocking;

    const string ATTACK_A = "Attack A";
    const string BLOCKING = "Blocking";

    void Awake()
    {
        combInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isBlocking = false;
        canAttack = true;
        attackHitBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Character_Moviment.moveInstance.grounded)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (!isAttacking)
                {
                    Character_Animation_Manager.animInstance.ChangeAnimationState(ATTACK_A);
                    canAttack = false;
                    isAttacking = true;
                }
            }

            if (Input.GetButtonDown("Block"))
            {
                isBlocking = true;
                Character_Animation_Manager.animInstance.ChangeAnimationState(BLOCKING);
            }

        }

    }
}