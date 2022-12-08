using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Observer
{
    void Start()
    {
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    public override void OnNotify(object value, int collValue, NotificationType notificationType)
    {
        if (notificationType == NotificationType.Hit && collValue != LayerMask.NameToLayer("Items"))
        {
            transform.Find("Hit").gameObject.GetComponent<AudioSource>().Play();
        }
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
        else
        {
            if (walk.isPlaying)
            {
                walk.Stop();
            }
        }
    }
}
