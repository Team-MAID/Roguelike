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
            Debug.Log("HIT");
        }
        else if (notificationType == NotificationType.Hit && value.ToString() == "Player")
        {
            transform.Find("Collect").gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("COLLECT");
        }

        AudioSource walk = transform.Find("Walk").gameObject.GetComponent<AudioSource>();

        if (notificationType == NotificationType.Move)
        {
            Debug.Log("ah");
           
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
