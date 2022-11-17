using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : Enemy
{
    private bool following = false;
    private float attackRange;
    private Vector2 movement;

    // Start is called before the first frame update

    public virtual void setAttackrange(float t_attackRange)
    {
        attackRange = t_attackRange;
    }

    public virtual void followMovement(ref Transform playerTransform, ref Rigidbody2D rb)
    {
        if (isAlive())
        {
            StartCoroutine(Checks(playerTransform, rb));
            if (following)
            {
                movement = playerTransform.position - transform.position;
                movement = movement.normalized;
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

                if (movement.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(-1.2f, 1.2f, 1.0f);
                }
                else if (movement.x < 0)
                {
                    gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                }
            }
        }
    }

    bool CheckDistance(Transform playerTransform, Rigidbody2D rb)
    {
        // loop through enemies
        if (Vector2.Distance(rb.transform.position, playerTransform.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public IEnumerator Checks(Transform playerTransform, Rigidbody2D rb)
    {
        //loop
       // Debug.Log("Coroutine Ran");
        if(CheckDistance(playerTransform, rb))
        {
            following = true;
        }
        else
        {
            following = false;
        }
        yield return new WaitForSeconds(4.0f);
    }
}
