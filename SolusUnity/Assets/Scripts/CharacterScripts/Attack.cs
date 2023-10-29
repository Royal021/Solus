using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{ 
   
    public int attackDamage = 10;

    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage damageable = collision.GetComponent<TakeDamage>();

        if (damageable != null)
        {

            Vector2 directionalKnockback = transform.parent.localScale.x > 0 ? knockback: new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage, directionalKnockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " was hit for " + attackDamage + " damage");
            }
            else
            {
                Debug.Log(collision.name + " was not hit");
            }          
        }
    }
    
}
