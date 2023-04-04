using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemente : MonoBehaviour
{
    // SideMovement and animation variables
    float sideSpeed = 1f;
    public Rigidbody2D player;
    public Animator animator;
    float horizontalMove = 0f;

    // Jump and Jump parameters
    public float jumpforce = 1f;
    private bool isgrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private int extraJumps;
    private bool facingRight = true;
    bool jumpRequest;

    void Update()
    {
     isgrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
     if (isgrounded == true)
        {
            OnLanding();
            extraJumps = 1;
        }  
     if(facingRight == false && horizontalMove > 0)
        {
            Flip();
        }else if(facingRight == true && horizontalMove < 0)
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            jumpRequest = true;
            extraJumps--;
            animator.SetBool("Jump", false);
            animator.SetBool("Jump", true);
        }
        else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isgrounded == true)
        {
            jumpRequest = true;
            player.velocity = Vector2.up * jumpforce;
            animator.SetBool("Jump", true);
        }

    }
  
    void FixedUpdate()
    {
        // Side and animation Movement
        horizontalMove = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
         player.velocity = new Vector2(horizontalMove * sideSpeed, player.velocity.y);
       
        // Jump Mechanics with extra jumps
       if(jumpRequest == true)
        {
            player.velocity = Vector2.up * jumpforce;
            jumpRequest = false;
        }
    }
    
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }




}
