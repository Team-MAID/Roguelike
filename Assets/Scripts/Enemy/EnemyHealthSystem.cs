using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int _health;
    private Color originalColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Save the original color of the game object
            originalColor = renderer.material.color;
        }
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
        TakeDamage();
        Debug.Log("Enemy Health down");
    }

    void TakeDamage()
    {
        // Get the renderer component of the game object
        Renderer renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Start a coroutine that flashes the game object red and back to its original color
            StartCoroutine(FlashColor(renderer));
        }
    }

    IEnumerator FlashColor(Renderer renderer)
    {
        // Set the game object's color to red
        renderer.material.color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Set the game object's color back to its original color
        renderer.material.color = originalColor;
    }
}
