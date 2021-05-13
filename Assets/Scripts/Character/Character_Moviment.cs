using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moviment : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float maxMoveAngle = 45f;
    float groundAngle;
    float moveInput;
    bool isFlip = false;
    Vector2 moveDirection;
    Vector2 facingSide = new Vector2(1, 0);

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float groundRaySize;
    [SerializeField] LayerMask whatIsGround;
    [HideInInspector] public bool grounded;

    [Header("Wall Jump")]
    [SerializeField] float wallJumpForce;
    [SerializeField] float wallRaySize;
    [SerializeField] float slideSpeed;
    [SerializeField] float slideTime;
    [SerializeField] LayerMask whatIsWall;
    [HideInInspector] public bool wallSliding;
    float slideTimer;

    Rigidbody2D rb;
    Animator anim;
    void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        slideTimer = slideTime;
    }
    void Update() {
        moveInput = Input.GetAxisRaw("Horizontal");

        CheckRaycasts();
        AnimationSet();

        if (moveInput != 0 && !wallSliding)
            Move();

        if (Input.GetButtonDown("Jump") && grounded && !wallSliding)
            Jump();

        if (Input.GetButtonDown("Jump") && !grounded && wallSliding)
            WallJump();

        if (wallSliding && Input.GetKeyDown(KeyCode.LeftShift)) { 
            rb.AddForce(new Vector2(-facingSide.x * 2, 0),ForceMode2D.Impulse);
            Flip();
        }
    }

    void CheckRaycasts() {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, groundRaySize, whatIsGround);

        RaycastHit2D hitAngle = Physics2D.Raycast(transform.position, moveDirection, 0.5f, whatIsGround);
        groundAngle = Vector2.Angle(hitAngle.normal, Vector2.up);

        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, facingSide, wallRaySize, whatIsWall);
        if (hitWall.collider != null && !grounded && rb.velocity.y < slideSpeed) { 
            wallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }
        else
            wallSliding = false;        

        if (hitGround.collider != null) {
            grounded = true;
            if (moveInput >= 0) 
                moveDirection = new Vector2(hitGround.normal.y, -hitGround.normal.x);            
            else
                moveDirection = new Vector2(hitGround.normal.y, hitGround.normal.x);           
        }
        else
            grounded = false;       

    }
    void Move() {         
        if (groundAngle < maxMoveAngle)            
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);        

        if (moveInput < 0 && !isFlip && !wallSliding) 
            Flip();        
        else if (moveInput > 0 && isFlip && !wallSliding) 
            Flip();
    }
    void Flip() {
        isFlip = !isFlip;
        facingSide = -facingSide;
        transform.Rotate(0,180,0);
    }

    void Jump() {        
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);        
    }

    void WallJump() {
        rb.AddForce(new Vector2(wallJumpForce * -facingSide.x, jumpForce),ForceMode2D.Impulse);
        Flip();        
    }

    void AnimationSet()
    {
        if (moveInput != 0 && !wallSliding)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);

        if (!grounded && rb.velocity.y > 0.02f)
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);

        if (!grounded && rb.velocity.y < -0.02f)
            anim.SetBool("Fall", true);
        else
            anim.SetBool("Fall", false);

        if (wallSliding)
            anim.SetBool("WallSlide", true);
        else
            anim.SetBool("WallSlide", false);


    }
    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallRaySize * facingSide.x, transform.position.y,transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y -groundRaySize, transform.position.z));
    }
}
