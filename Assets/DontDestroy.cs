using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
        // Destroy when cleared to avoid duplicate players and companions on restart
        if (SceneManager.GetActiveScene().name == "Cleared")
        {
            Destroy(this.gameObject);
        }
        if (_player == null)
        {
            Debug.Log("Arrived");
            Destroy(this.gameObject);
        }
    }
}
