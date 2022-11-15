using UnityEngine;

public class PointOfInterest : Subject
{
    [SerializeField]
    private string poiName;
    private void Start()
    {
        poiName = gameObject.name;
    }
    private void OnTriggerEnter2D(Collider2D _otherColldier)
    {
        Notify(poiName, _otherColldier.gameObject.layer, NotificationType.Hit);
    }
    private void FixedUpdate()
    {
        int horizontalInput = (int)Input.GetAxisRaw("Horizontal");
        int verticalInput = (int)Input.GetAxisRaw("Vertical");

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