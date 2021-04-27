using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Moviment : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float groundRaySize;
    public float wallRaySize;
    public float slideSpeed;
    public float slideTime;
    public float slideTimer;
    float maxMoveAngle = 45f;
    float groundAngle;
    float moveInput;
    
    bool grounded;
    public bool wallSliding;
    bool isFlip = false;
    Vector2 moveDirection;
    Vector2 facingSide = new Vector2(1,0);    
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        slideTimer = slideTime;
    }

    void FixedUpdate() {
        if (wallSliding) {
            slideTimer -= Time.fixedDeltaTime;
            if (slideTimer < 0) {
                Move();
                slideTimer = slideTime;
            }
        }
        else {
            Move();
            slideTimer = slideTime;
        }
    }

    void Update() {
        Jump();
        WallJump();
    }

    void Move() {
        moveInput = Input.GetAxisRaw("Horizontal");        

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, groundRaySize, whatIsGround);
        if (hitGround.collider != null) {            
            grounded = true;
            if (moveInput >= 0) {
                moveDirection = new Vector2(hitGround.normal.y, -hitGround.normal.x);
            }
            else{
                moveDirection = new Vector2(hitGround.normal.y, hitGround.normal.x);
            }
        }
        else {
            grounded = false;
        }

        RaycastHit2D hitAngle = Physics2D.Raycast(transform.position, moveDirection, 0.5f, whatIsGround);        
        groundAngle = Vector2.Angle(hitAngle.normal, Vector2.up);
        if (groundAngle < maxMoveAngle) {            
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (moveInput < 0 && !isFlip && !wallSliding) {
            Flip();
        }
        else if (moveInput > 0 && isFlip && !wallSliding) {
            Flip();
        }

    }
    void Flip() {
        isFlip = !isFlip;
        facingSide = -facingSide;
        transform.Rotate(0,180,0);
    }

    void Jump() {
        if (Input.GetButtonDown("Jump") && grounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void WallJump() {
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, facingSide, wallRaySize, whatIsWall);
        if (hitWall.collider != null && !grounded && rb.velocity.y < slideSpeed) {
            wallSliding = true;                        
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }
        else {
            wallSliding = false;
        }

        if (Input.GetButtonDown("Jump") && wallSliding) {           
            rb.AddForce(new Vector2(jumpForce * moveInput, 1 * jumpForce),ForceMode2D.Impulse);            
        }
    }
    void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallRaySize * facingSide.x, transform.position.y,transform.position.z));
  
    }
}
