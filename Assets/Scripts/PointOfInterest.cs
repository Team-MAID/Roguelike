using UnityEngine;

public class PointOfInterest : Subject
{
    [SerializeField]
    private string poiName;
    private void Start()
    {
        poiName = gameObject.name;
    }
    private void Update()
    {
        /// <summary>
        /// When the gameobject cannot be found, meaning the object died and has been destroyed, Die notification is sent through to pause any audio that may be looping
        /// </summary>
        
        if (this.gameObject == null)
        {
            Notify(poiName, 0, NotificationType.Die);
        }
    }
    /// <summary>
    /// Sends a Hit notification when the gameobject interacts with a trigger object
    /// </summary>
    private void OnTriggerEnter2D(Collider2D _otherColldier)
    {
        Notify(poiName, _otherColldier.gameObject.layer, NotificationType.Hit);
    }
    /// <summary>
    /// Sends a Hit notification when the gameobject interacts with a collider object
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Notify(poiName, collision.gameObject.layer, NotificationType.Hit);
    }
    private void FixedUpdate()
    {
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

        /// <summary>
        /// Sends a Move notification when the gameobject moves in any direction, which will start the walking audio
        /// </summary>
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Notify(poiName, 0, NotificationType.Move);
        }
        else
        {
            Notify(poiName, 0, NotificationType.Stop);
        }

    }
}
