using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector2 _velocity = Vector2.zero;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector2 playerPos = player.transform.position;

        Vector2 newCameraPos = Vector2.SmoothDamp(transform.position, playerPos, ref _velocity, 0.3f, Mathf.Infinity, Time.fixedDeltaTime);
        
        transform.position = new Vector3(newCameraPos.x, newCameraPos.y, -10);
    }
}
