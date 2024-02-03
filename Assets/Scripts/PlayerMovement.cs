using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //calling unity components to use them as functions
    private Rigidbody2D boss;
    private Animator animate;
    private BoxCollider2D coll; 
    private SpriteRenderer sprite;

    private float dirX = 0;
    
    //adding SerializedField can allow us to edit these variables directly in Unity
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;
    [SerializeField]private LayerMask jumpableGround;

    private enum MovementState{idle,running,jumping,falling}    //0 = idle, 1 = running, 2 = jumping, 3 = falling
    

    // we have to call the component functions at the start to use them
    private void Start()
    {
        boss = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        boss.velocity = new Vector2(dirX*moveSpeed , boss.velocity.y);
        if (Input.GetButtonDown("Jump")&&IsGrounded())
        {
            boss.velocity = new Vector2(boss.velocity.x, jumpForce);
        }

        UpdateAnimationState();


    }
    //this function update the player state, jumping, running, etc, 
    //we will use this function to determine the animations and other things
    private void UpdateAnimationState()
    {

        MovementState state;
        //running right
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        //running left
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        //idle
        else
        {
            state = MovementState.idle;
        }

        //jumpping
        if (boss.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if(boss.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        animate.SetInteger("state", (int)state);
    }
    //check if player on the ground
    //a bool function
    private bool IsGrounded()
    {
        //casting a box around the box collider
        //the box is centered,same size as the bounds of the collider, and is only dectecting downward motion
        //downwards so iff player is standing on the box, eg. if the player hits the wall it wouldn't count as down
        //using jumpableGround in unity, toggle
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, .1f, jumpableGround);
     
    }
}
