using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int _health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (_health <= 0)
        //{
        //    Debug.Log("Enemy Destroyed by Health System");
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        
    }
    public void decreaseHealth()
    {
        _health--;
        Debug.Log("Enemy Health down");
    }
}
