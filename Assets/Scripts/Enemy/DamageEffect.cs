using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>DamageEffect</c> Renders a damage effect to be used on the player and enemies
/// </summary>
public class DamageEffect : MonoBehaviour
{
    private Color originalColor;
    public bool isImmuneToDamage;
    private Renderer renderer;

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

    public void TakeDamageEffect(float t_immuneFor)
    {
        // Get the renderer component of the game object
        renderer = GetComponent<Renderer>();

        // Check if the renderer is not null
        if (renderer != null)
        {
            // Start a coroutine that flashes the game object red and back to its original color
            StartCoroutine(FlashColor(renderer, t_immuneFor));
        }
    }

    /// <summary>
    /// Method <c>FlashColor</c> Set the color of the gameObject to be red 
    /// then wait for a period of time before going back to the gameObject original color 
    /// </summary>
    IEnumerator FlashColor(Renderer renderer, float t_immuneFor)
    {
        isImmuneToDamage = true;
        // Set the game object's color to red
        renderer.material.color = Color.red;
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(t_immuneFor);
        isImmuneToDamage = false;
        // Set the game object's color back to its original color
        renderer.material.color = originalColor;
	}
}
