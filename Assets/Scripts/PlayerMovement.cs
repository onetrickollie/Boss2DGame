using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //calling rigibody2d to get the component
    private Rigidbody2D boss;
    private Animator animate;

    private SpriteRenderer sprite;
    private float dirX = 0;
    //adding SerializedField can allow us to edit these variables directly in Unity
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    private enum MovementState{idle,running,jumping,falling}    //0 = idle, 1 = running, 2 = jumping, 3 = falling
    

    // Start is called before the first frame update
    private void Start()
    {
        boss = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        boss.velocity = new Vector2(dirX*moveSpeed , boss.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            boss.velocity = new Vector2(boss.velocity.x, jumpForce);
        }

        UpdateAnimationState();


    }
  
    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

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
}
