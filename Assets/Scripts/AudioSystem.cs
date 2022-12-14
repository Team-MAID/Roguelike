using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Observer
{
    void Start()
    {
        /// <summary>
        /// Finds all objects of type PointOfInterest and registers them to the observer
        /// </summary>
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    /// <summary>
    /// Is called from a PointOfInterest script, takes in the name of the object, the layer that it collided with and the type of notification sent through
    /// </summary>
    public override void OnNotify(object value, int collValue, NotificationType notificationType)
    {
        /// <summary>
        /// Plays the 'Hit' audio when the player interacts with anything not collectible
        /// </summary>
        if (notificationType == NotificationType.Hit && collValue != LayerMask.NameToLayer("Items"))
        {
            transform.Find("Hit").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'Collect' audio when the player interacts with a collectible 
        /// </summary>
        else if (notificationType == NotificationType.Hit && value.ToString() == "Player" && collValue == LayerMask.NameToLayer("Items"))
        {
            transform.Find("Collect").gameObject.GetComponent<AudioSource>().Play();
        }

        AudioSource walk = transform.Find("Walk").gameObject.GetComponent<AudioSource>();

        if (notificationType == NotificationType.Move)
        {  
            if (!walk.isPlaying)
            {
                walk.Play();
            }
        }
        /// <summary>
        /// If any other notification type is sent through, the player walking sound stops
        /// </summary>
        else
        {
            if (walk.isPlaying)
            {
                walk.Stop();
            }
        }
    }
}
