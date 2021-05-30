using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Moviment : MonoBehaviour
{
    enum Moviment { Patrol, Guard };

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
        
    }

    void Chase()
    {
        
    }

    void Guard()
    {
        
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
