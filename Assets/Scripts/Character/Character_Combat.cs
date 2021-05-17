using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Combat : MonoBehaviour
{
    public static Character_Combat combInstance;

    public bool canAttack;
    public bool isAttacking;

    Animator anim;

    void Awake()
    {
        combInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        canAttack = true;
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
                    Character_Animation_Manager.animInstance.ChangeAnimationState("Attack A");
                    canAttack = false;
                    isAttacking = true;
                }
            }
            else
                return;
        }
    }
}
