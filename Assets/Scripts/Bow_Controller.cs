using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow_Controller : MonoBehaviour
{
    public GameObject _projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("LeftClick");

            Instantiate(_projectile, this.transform.position, Quaternion.identity);

        }
    }
}
