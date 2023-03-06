using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer spriteR;
    private BoxCollider2D coll;
    private float dirX;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpHeight = 12f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementType { idle, run, jump, fall };
    

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");               //GetAxisRaw - no slide
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        MovementType CurrentState;
        if (dirX > 0f)
        {
            CurrentState = MovementType.run;
            spriteR.flipX = false;
        }
        else if (dirX < 0f)
        {
            CurrentState = MovementType.run;
            spriteR.flipX = true;
        }
        else
        {
            CurrentState = MovementType.idle;
        }

        if (rb.velocity.y > .1f)
        {
            CurrentState = MovementType.jump;
        }  
        else if (rb.velocity.y < -.1f)
        {
            CurrentState = MovementType.fall;
        }

        ani.SetInteger("CurrentState", (int)CurrentState);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
