using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerController _playerController; 
    
    void Start()
    {
        _playerController = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        Vector2 playerPos = player.transform.position;

        Vector2 newCameraPos = Vector2.Lerp(transform.position, playerPos, (_playerController.MaxSpeed - 5) * Time.fixedDeltaTime);
        transform.position = new Vector3(newCameraPos.x, newCameraPos.y, -10);
    }
}
