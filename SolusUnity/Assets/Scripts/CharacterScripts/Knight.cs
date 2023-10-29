using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(TakeDamage))]
public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public detectionZone attackZone;


    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    TakeDamage takeDamage;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if (value == WalkableDirection.Left)
                { 
                    walkDirectionVector = Vector2.left; 
                }
            }
            _walkDirection = value; }
    }

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget,value);
        }
    }


    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        takeDamage = GetComponent<TakeDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        if (!takeDamage.LockVelocity)
        {
            if (canMove)
            {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if(walkDirection== WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }
        else if(walkDirection == WalkableDirection.Left)
        {  
            walkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direciton is not legal, its not right or lefft");
        }
    }

 
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}
