using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_game : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            var otherScript = FindObjectOfType<raketa_Controller>();

            if (otherScript.received_message == "start")
            {
                Debug.Log("kdlfl");
                //SceneManager.LoadScene("main_scene");
            }
        }
    }
}
