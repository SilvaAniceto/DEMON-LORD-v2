using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Behaviour : MonoBehaviour
{
    public static Slime_Behaviour slimeMov;

    [SerializeField] float movespeed;
    [SerializeField] float groundRaySize;
    [SerializeField] Transform checkEdges;
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsPlayer;
    [HideInInspector] public bool isAttacking;
    float attackTimer;

    Rigidbody2D rb;
    Animator animator;
    RaycastHit2D hitGround;

    void Awake()
    {
        slimeMov = this;       
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        attackTimer = Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        hitGround = Physics2D.Raycast(checkEdges.position, Vector2.down, groundRaySize, whatIsGround);
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            attackTimer = Random.Range(2, 5);
        }
        else
        {
            if (hitGround.collider != null)
                rb.velocity = new Vector2(movespeed, rb.velocity.y);
            else
                Flip();
        }
    }

    void Flip()
    {
        movespeed *= -1;
        transform.Rotate(0, 180, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkEdges.position, new Vector3(checkEdges.position.x, checkEdges.position.y - groundRaySize, checkEdges.position.z));
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void AttackDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position,attackRange, whatIsPlayer);
        if (hit != null)
        {
            Character_Health_Manager.health.HealthManagement(1);
        }
    }
}
