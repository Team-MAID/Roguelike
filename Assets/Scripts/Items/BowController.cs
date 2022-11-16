using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("LeftClick");

            Instantiate(projectile, this.transform.position, Quaternion.identity);

        }
    }
}
