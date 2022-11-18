using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour
{

    void OnDestroy()// When this gets destroyed, destroy the parent gameObject (the actual Ghost object).
    {
        Destroy(transform.parent.gameObject);
    }
}
