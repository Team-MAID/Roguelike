using UnityEngine;

/// <summary>
/// Allows a GameObject to collect an item from the ground when colliding with it
/// </summary>
public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the Game Object we collided with implement the ICollectable interface
        ICollectable collectable = col.GetComponent<ICollectable>();
        collectable?.Collect();
    }
}