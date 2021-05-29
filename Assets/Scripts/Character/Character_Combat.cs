using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Combat : MonoBehaviour
{
    public static Character_Combat combInstance;

    public bool canAttack;
    public bool isAttacking;
    public CircleCollider2D attackHitBox;
    public CapsuleCollider2D upperBody;

    public bool isBlocking;
    public bool isRolling;

    const string ATTACK_A = "Attack A";
    const string BLOCKING = "Blocking";
    const string ROLLING = "Rolling";

    Rigidbody2D rb;

    void Awake()
    {
        combInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        isRolling = false;
        isBlocking = false;
        canAttack = true;
        attackHitBox.enabled = false;
        rb = this.GetComponent<Rigidbody2D>();
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
                if (!isBlocking)
                {
                    isBlocking = true;
                    isAttacking = false;
                    Character_Animation_Manager.animInstance.ChangeAnimationState(BLOCKING);
                }
            }

            if (Input.GetButtonDown("Roll"))
            {
                if (!isRolling)
                {
                    isRolling = true;
                    isAttacking = false;
                    upperBody.enabled = false;
                    Character_Animation_Manager.animInstance.ChangeAnimationState(ROLLING);
                    rb.AddForce(Character_Moviment.moveInstance.facingSide * 25f, ForceMode2D.Impulse);
                }
            }
        }
    }
}