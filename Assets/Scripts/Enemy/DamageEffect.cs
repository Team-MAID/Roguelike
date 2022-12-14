using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private Color originalColor;
    public bool isImmuneToDamage;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        isImmuneToDamage = false;
        // Check if the renderer is not null
        if (renderer != null)
        {
            // Save the original color of the game object
            originalColor = renderer.material.color;
        }
    }

    public void TakeDamageEffect()
    {
        // Get the renderer component of the game object
        renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Start a coroutine that flashes the game object red and back to its original color
            StartCoroutine(FlashColor(renderer));
        }
    }

    IEnumerator FlashColor(Renderer renderer)
    {
        isImmuneToDamage = true;
        // Set the game object's color to red
        renderer.material.color = Color.red;
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(1.0f);
        isImmuneToDamage = false;
        // Set the game object's color back to its original color
        renderer.material.color = originalColor;
	}
}
