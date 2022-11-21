using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [field: SerializeField] public GameObject Player { get; set; }

    private Vector2 _velocity = Vector2.zero;

    void FixedUpdate()
    {
        if (Player != null)
        {
            Vector2 playerPos = Player.transform.position;

            Vector2 newCameraPos = Vector2.SmoothDamp(transform.position, playerPos, ref _velocity, 0.3f, Mathf.Infinity, Time.fixedDeltaTime);

            transform.position = new Vector3(newCameraPos.x, newCameraPos.y, -10);
        }
    }
}
