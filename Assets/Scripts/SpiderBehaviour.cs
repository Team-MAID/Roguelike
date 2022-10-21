using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    private bool following = false;

    [SerializeField]
    private GameObject player;

    private Vector2 movement;
    public float attackRange = 5.0f;
    public int speed = 2;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Checks());
        if (following)
        {
            movement = player.transform.position - transform.position;
            movement = movement.normalized;
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        { 

        }
    }

    bool CheckDistance()
    {
        // loop through enemies
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator Checks()
    {
        //loop
        Debug.Log("Coroutine Ran");
        if(CheckDistance())
        {
            following = true;
        }
        else
        {
            following = false;
        }
        yield return new WaitForSeconds(5.1f);
    }
}
