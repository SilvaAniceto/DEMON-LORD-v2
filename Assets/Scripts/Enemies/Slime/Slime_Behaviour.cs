using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Behaviour : MonoBehaviour {
    public static Slime_Behaviour slimeMov;

    [SerializeField] float movespeed;
    [SerializeField] float groundRaySize;
    [SerializeField] float wallCircle;
    [SerializeField] Transform checkEdges;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsWall;

    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsPlayer;
    [HideInInspector] public bool isAttacking;
    float attackTimer;
    int attackDir = 1;

    Rigidbody2D rb;
    Animator animator;
    RaycastHit2D hitGround;
    Collider2D hitWall;

    void Awake() {
        slimeMov = this;
    }
    // Start is called before the first frame update
    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        attackTimer = Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update() {
        hitGround = Physics2D.Raycast(checkEdges.position, Vector2.down, groundRaySize, whatIsGround);
        hitWall = Physics2D.OverlapCircle(checkEdges.position, wallCircle, whatIsWall);
        attackTimer -= Time.deltaTime;

        if (hitWall != null)
            Flip();

        if (attackTimer <= 0) {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            attackTimer = Random.Range(2, 5);
        }
        else {
            if (hitGround.collider != null && Enemy_Health_Manager.enemyHealth.isHit == false)
                rb.velocity = new Vector2(movespeed, rb.velocity.y);
            else if (hitGround.collider == null)
                Flip();
        }
    }

    void Flip() {
        attackDir *= -1;
        movespeed *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void AttackDamage() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
        if (hit != null) {
            Character_Health_Manager.health.HealthManagement(1, attackDir);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Character_Health_Manager.health.HealthManagement(1, attackDir);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(checkEdges.position, new Vector3(checkEdges.position.x, checkEdges.position.y - groundRaySize, checkEdges.position.z));
        Gizmos.DrawWireSphere(checkEdges.position, wallCircle);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
