using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Moviment : MonoBehaviour
{
    enum Moviment { Patrol, Chase, Patrol_and_Chase };

    [Header("Moviment")]
    [SerializeField] Moviment movimentType;
    [SerializeField] float movespeed;
    [SerializeField] float groundRaySize;
    [SerializeField] Transform checkEdges;
    [SerializeField] LayerMask whatIsGround;
   
    [Header("Line of Sight")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] Transform backRay;
    [SerializeField] Transform frontRay;
    float sightRange;
    float backSightRange;
    RaycastHit2D hitGround;
    RaycastHit2D backSight;
    RaycastHit2D sight;

    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayCasts();

        if (movimentType == Moviment.Patrol)
            Patrol();
        else if (movimentType == Moviment.Chase)
            Chase();
        else if (movimentType == Moviment.Patrol_and_Chase)
            PatrolAndChase();

    }

    void CheckRayCasts()
    {
        hitGround = Physics2D.Raycast(checkEdges.position, Vector2.down, groundRaySize, whatIsGround);

        backSightRange = transform.position.x - backRay.position.x;
        backSight = Physics2D.Raycast(transform.position, Vector2.left, backSightRange, whatIsPlayer);

        sightRange = frontRay.position.x - transform.position.x;
        sight = Physics2D.Raycast(transform.position, Vector2.right, sightRange, whatIsPlayer);
    }

    void Patrol()
    {
        if (hitGround.collider != null)
            rb.velocity = new Vector2(movespeed, rb.velocity.y);
        else
            Flip();
    }

    void Chase()
    {        
        if (sight.collider != null)
        {
            if (hitGround.collider == null)
                return;
            else
                rb.velocity = new Vector2(movespeed, rb.velocity.y);
        }
        else if (backSight.collider != null)
            Flip();        
    }

    void PatrolAndChase()
    {
        if (sight.collider != null)
            rb.velocity = new Vector2(movespeed, rb.velocity.y);
        else if (backSight.collider != null)
            Flip();
        else
            Patrol();
    }

    void Flip()
    {
        movespeed *= -1;
        transform.Rotate(0, 180, 0);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, backRay.position);
        Gizmos.DrawLine(transform.position, frontRay.position);
        Gizmos.DrawLine(checkEdges.position, new Vector3(checkEdges.position.x, checkEdges.position.y - groundRaySize, checkEdges.position.z));
    }
}
