using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Combat : MonoBehaviour {
    public static Character_Combat combInstance;

    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool isAttacking;
    [SerializeField] float attackRange;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask whatIsEnemy;
    public CapsuleCollider2D upperBody;

    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool isRolling;

    const string ATTACK_A = "Attack A";
    const string BLOCKING = "Blocking";
    const string ROLLING = "Rolling";
    const string DODGE = "Dodge";

    Rigidbody2D rb;

    void Awake() {
        combInstance = this;
    }
    // Start is called before the first frame update
    void Start() {
        isRolling = false;
        isBlocking = false;
        canAttack = true;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Character_Moviment.moveInstance.grounded) {
            if (Input.GetButtonDown("Attack")) {
                if (!isAttacking) {
                    Character_Animation_Manager.animInstance.ChangeAnimationState(ATTACK_A);
                    canAttack = false;
                    isAttacking = true;
                }
            }

            if (Input.GetButtonDown("Block")) {
                if (!isBlocking) {
                    isBlocking = true;
                    isAttacking = false;
                    Character_Animation_Manager.animInstance.ChangeAnimationState(BLOCKING);
                }
            }

            if (Input.GetButtonDown("Roll")) {
                if (!isRolling) {
                    isRolling = true;
                    isAttacking = false;
                    upperBody.enabled = false;
                    if (Character_Moviment.moveInstance.moveInput != 0)
                    {
                        Character_Animation_Manager.animInstance.ChangeAnimationState(ROLLING);
                        rb.AddForce(Character_Moviment.moveInstance.facingSide * 18f, ForceMode2D.Impulse);
                    }
                    else
                    {
                        Character_Animation_Manager.animInstance.ChangeAnimationState(DODGE);
                        rb.AddForce(new Vector2(10 * -Character_Moviment.moveInstance.facingSide.x, 10), ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    public void DealDamage() {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);
        foreach (Collider2D e in hit) {
            if (hit != null) {
                Enemy_Health_Manager.enemyHealth.EnemyHealthManagement(1);
            }

        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}