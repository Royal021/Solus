using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;

    public enum WalkableDirection { Left, Right }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection == value)
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
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
    }

    private void FixedUpdate()
    {
        if(touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed *walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(walkDirection== WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }else if(walkDirection == WalkableDirection.Left)
        {  
            walkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direciton is not legal, its not right or lefft");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
