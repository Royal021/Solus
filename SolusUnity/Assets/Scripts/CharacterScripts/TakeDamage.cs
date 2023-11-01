using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeDamage : MonoBehaviour
{
    public UnityEvent<int, Vector2> dHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int maxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _currentHealth =100;

    public int currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            if(_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;



    private float timeSinceHit = 0;
    public float invincibilityTime =  0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set to " + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }



    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if(isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
      
    }

    //returns true if the object was hit
    public bool Hit(int damage, Vector2 knockback)
    {
        //able to be hit
        if (IsAlive && !isInvincible)
        {

            currentHealth -= damage;
            isInvincible = true;
            // notify other components that the damageable was hit and knocked back was handled
           animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            dHit?.Invoke(damage, knockback);
            CharacterEvents.charDamaged.Invoke(gameObject, damage);

            return true;
        }
        //unable to be hit
        return false;
    }
}
